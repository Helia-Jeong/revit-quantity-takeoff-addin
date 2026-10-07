using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
// WinForms 와 Revit API 에 같은 이름이 있는 클래스는 어느 쪽을 쓸지 지정합니다.
using TaskDialog = Autodesk.Revit.UI.TaskDialog;

namespace Modless
{
    // ═══════════════════════════════════════════════════════════════
    //  ExternalEvent 란?
    // ═══════════════════════════════════════════════════════════════
    //
    //  ■ 왜 필요한가?
    //    Revit 은 "지금은 애드인 차례" 라고 허락한 동안에만 Revit API 를 쓸 수 있게 합니다.
    //    (이 "애드인 차례" 를 API 컨텍스트 라고 부릅니다.)
    //    명령(Command)의 Execute() 가 실행되는 동안이 바로 "애드인 차례" 입니다.
    //
    //    [모달 폼 - ShowDialog()]
    //      명령 시작 → 폼 열림 → 버튼 클릭 → 폼 닫힘 → 명령 끝
    //      버튼을 누를 때 명령이 아직 안 끝났음 → "애드인 차례" → API 사용 O
    //      (대신 폼이 열려 있는 동안 Revit 을 조작할 수 없습니다)
    //
    //    [모드리스 폼 - Show()]
    //      명령 시작 → 폼 열림 → 명령 끝 (폼은 계속 떠 있음) → 버튼 클릭
    //      버튼을 누를 때 명령이 이미 끝났음 → "애드인 차례" 아님 → API 사용 X
    //      (억지로 사용하면 "... outside of API context is not allowed." 예외 발생)
    //
    //    비유 : 은행 창구
    //      창구 직원(Revit)은 번호표를 뽑고 내 차례가 된 손님만 업무를 봐 줍니다.
    //      ExternalEvent 가 바로 이 "번호표" 입니다.
    //
    //  ■ 해결 방법 : ExternalEvent (번호표)
    //    버튼을 누르면 번호표를 뽑아 두고, 내 차례가 되면 Revit 이 할 일을 실행해 줍니다.
    //
    //    1) ExternalEvent.Create(this)    → 번호표 기계 설치 (폼을 만들 때 한 번)
    //    2) exEvent.Raise()               → 번호표 뽑기 (버튼 클릭 시)
    //    3) Execute(UIApplication app)    → 내 차례! Revit 이 호출해 줌 (API 사용 O)
    //    4) exEvent.Dispose()             → 번호표 기계 철거 (폼을 닫을 때)
    //
    //    ※ 번호표를 뽑는다고 바로 실행되지 않습니다. 차례가 올 때까지 기다립니다.
    //
    //  ■ 흐름
    //    [버튼 클릭] → 번호표 뽑기 → (차례 기다림) → 내 차례 → { } 안의 코드 실행
    //
    //  ■ 사용 방법
    //    버튼 안에 아래 모양을 그대로 쓰고, { } 안에 할 일을 작성하면 됩니다.
    //
    //        RunRevit((uidoc, doc) =>
    //        {
    //            // 할 일
    //        });
    //
    // ═══════════════════════════════════════════════════════════════
    public partial class MainForm : System.Windows.Forms.Form, IExternalEventHandler
    {
        // 번호표 기계
        private readonly ExternalEvent _exEvent;

        // 번호표에 적어 둔 할 일 (내 차례가 되면 실행됨)
        private Action<UIDocument, Document> _action;

        public static string m_floortypename = "";//빈공간 만들어주기
        public static string m_walltypename = "";
        public static string m_wallheight = "";
        public static string m_ceilingtypename = "";
        public static string m_ceilingheight = "";
        public static string m_doortypename = "";
        public static string m_beamtypename = "";
        public static string m_coltypename = "";

        public static Level m_BottomLevel = null;
        public static string m_BottomLevelSTR = "";

        public static Level m_TopLevel = null;
        public static string m_TopLevelSTR = "";


        public static bool m_floor_checked = false;
        public static bool m_wall_checked = false;
        public static bool m_ceiling_checked = false;


        public MainForm()
        {
            InitializeComponent();//폼 버튼들 초기화

            // 번호표 기계 설치
            // (폼은 Command.Execute() 안, 즉 "애드인 차례" 에 만들어지므로 여기서 설치할 수 있습니다)
            _exEvent = ExternalEvent.Create(this);
        }

        // ───────────── 버튼 ─────────────

        private void btnRun_Click(object sender, EventArgs e)//바닥 생성
        {
            RunRevit((uidoc, doc) =>
            {
                List<FloorData> fl = FloorATT.GetFloorData(doc, uidoc, m_floortypename);

                foreach (FloorData f in fl)
                {
                    Util.CreateFloor(doc, f.m_CurveLoops, f.m_FloorType, f.m_level, f.m_FloorTypeTHK);
                }

            });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RunRevit((uidoc, doc) =>
            {
                // ▼ 여기에 Revit API 코드를 작성하세요.
                TaskDialog.Show("확인", "여기에 코드를 작성하시면 됩니다.");
            });
        }

        private void CreateWallByCL_Click(object sender, EventArgs e)//마감벽생성기
        {
            RunRevit((uidoc, doc) =>
            {
                Reference r = uidoc.Selection.PickObject(ObjectType.Face);
                Element e = doc.GetElement(r);
                Face f = e.GetGeometryObjectFromReference(r) as Face;//GeometryObject 형태로 받아져서 Face로 변환까지

                EdgeArrayArray eaa = f.EdgeLoops;

                List<CurveLoop> cls = new List<CurveLoop>();

                foreach (EdgeArray ea in eaa)
                {
                    CurveLoop cl = new CurveLoop();

                    foreach (Edge edge in ea)
                    {
                        cl.Append(edge.AsCurve());
                    }
                    cls.Add(cl);
                }

                CurveLoop firstloop = cls[0];//길이 소팅 없이 진행하는 버전
                WallType wt = Util.FindWallTypeByName(doc, m_walltypename);

                if (wt == null)
                {
                    TaskDialog.Show("오류", m_walltypename + "이(가) 없습니다.");
                    return;//그냥 끝내라
                }

                //List<FloorData> fl = FloorATT.GetFloorData(doc, uidoc, m_walltypename);
                //foreach (FloorData item in fl)
                //{
                //    IList<CurveLoop> cls = item.m_CurveLoops;
                // 이러고 커브 중 큰거 고른다음, 벽 두께만큼 안쪽으로 옵셋하고 벽세우기
                //}

                double thk = Util.GetWallTHK(wt);

                CurveLoop offLoop = CurveLoop.CreateViaOffset(firstloop, -thk / 2, XYZ.BasisZ);//절반만큼 안쪽으로, z방향으로

                //m_floortypename 가져와서 마감슬라브 두께 측정 후 wall 그릴 때 레벨로부터 띄우기 값 변경

                //int t = offLoop.NumberOfCurves();//커브 잘 뽑았나 개수 확인
                //Debug.Print(t.ToString());

                Level level = doc.ActiveView.GenLevel;
                double wallheight = Convert.ToDouble(m_wallheight);

                foreach (Curve c in offLoop)
                {
                    Util.CreateWall(doc, c, wt, level, wallheight, false);
                }

            });
        }

        private void MainForm_Load(object sender, EventArgs e)//폼이 로드될 때 뭐할꺼야? 콤보박스에 데이터 담아놓을거야
        {

            RunRevit((uidoc, doc) =>
            {
                FilteredElementCollector col = new FilteredElementCollector(doc);
                col.OfCategory(BuiltInCategory.OST_Floors);
                col.OfClass(typeof(FloorType));

                foreach (FloorType ft in col)
                {
                    string Name = ft.Name;
                    comboBox1.Items.Add(Name);//콤보박스에 담기
                }


                FilteredElementCollector col_beam = new FilteredElementCollector(doc);
                col_beam.OfCategory(BuiltInCategory.OST_StructuralFraming);
                col_beam.OfClass(typeof(FamilySymbol));

                foreach (FamilySymbol fs in col_beam)
                {
                    string Name = fs.Name;
                    comboBox2.Items.Add(Name);
                }


                FilteredElementCollector col_column = new FilteredElementCollector(doc);
                col_column.OfCategory(BuiltInCategory.OST_StructuralColumns);
                col_column.OfClass(typeof(FamilySymbol));

                foreach (FamilySymbol fs in col_column)
                {
                    string Name = fs.Name;
                    comboBox3.Items.Add(Name);
                }


                FilteredElementCollector col_wall = new FilteredElementCollector(doc);
                col_wall.OfCategory(BuiltInCategory.OST_Walls);
                col_wall.OfClass(typeof(WallType));

                foreach (WallType wt in col_wall)
                {
                    string Name = wt.Name;
                    comboBox4.Items.Add(Name);
                }


                FilteredElementCollector col_level = new FilteredElementCollector(doc);
                col_level.OfClass(typeof(Level));

                foreach (Level lv in col_level)
                {
                    string Name = lv.Name;
                    Level_Picker.Items.Add(Name);
                    Level_Top_Pick.Items.Add(Name);
                }

                FilteredElementCollector col_ceil = new FilteredElementCollector(doc);
                col_ceil.OfCategory(BuiltInCategory.OST_Ceilings);
                col_ceil.OfClass(typeof(CeilingType));

                foreach (CeilingType ct in col_ceil)
                {
                    string Name = ct.Name;
                    comboBox6.Items.Add(Name);
                }

                FilteredElementCollector col_door = new FilteredElementCollector(doc);
                col_door.OfCategory(BuiltInCategory.OST_Doors);
                col_door.OfClass(typeof(FamilySymbol));

                foreach (FamilySymbol dt in col_door)
                {
                    string Name = dt.Name;
                    comboBox7.Items.Add(Name);
                }


            });
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)//슬라브타입
        {
            m_floortypename = comboBox1.SelectedItem.ToString();
        }
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)//벽타입
        {
            m_walltypename = comboBox4.SelectedItem.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)//벽 두께 입력값
        {
            m_wallheight = textBox1.Text;
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)//data 선택 for 그리드뷰
        {
            RunRevit((uidoc, doc) =>
            {
                if (comboBox5.SelectedItem.ToString() == "Wall")
                {
                    List<string> strs = new List<string>
                    { "No.", "ID", "Level", "Name", "Length", "Area", "Start X", "Start Y", "Start Z", "End X", "End Y", "End Z"};
                    DataTable dt = Util.GetDataTableFromStrArr(strs);

                    FilteredElementCollector wall_col = new FilteredElementCollector(doc);
                    wall_col.OfCategory(BuiltInCategory.OST_Walls);
                    wall_col.OfClass(typeof(Wall));


                    int i = 0;
                    double sum = 0;
                    foreach (Wall wall in wall_col)
                    {
                        i++;
                        Parameter lenparam = wall.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH);
                        string len = lenparam.AsValueString();

                        Parameter areaparam = wall.get_Parameter(BuiltInParameter.HOST_AREA_COMPUTED);
                        double area = Math.Round(areaparam.AsDouble() * 0.09290304, 3);//반올림

                        Parameter levparam = wall.get_Parameter(BuiltInParameter.WALL_BASE_CONSTRAINT);
                        string level = levparam.AsValueString();

                        sum += area;

                        Location lt = wall.Location;
                        LocationCurve lc = lt as LocationCurve;
                        Curve c = lc.Curve;
                        XYZ sp = c.GetEndPoint(0);
                        XYZ ep = c.GetEndPoint(1);

                        DataRow dr = dt.NewRow();
                        dr[0] = i.ToString();
                        dr[1] = wall.Id.ToString();
                        dr[2] = level;
                        dr[3] = wall.Name;
                        dr[4] = len;
                        dr[5] = area.ToString();
                        //XYZ.ToString()은 쉼표가 들어가 CSV 열이 깨지므로 좌표를 X/Y/Z 열로 분리
                        dr[6] = sp.X.ToString("F9", CultureInfo.InvariantCulture);
                        dr[7] = sp.Y.ToString("F9", CultureInfo.InvariantCulture);
                        dr[8] = sp.Z.ToString("F9", CultureInfo.InvariantCulture);
                        dr[9] = ep.X.ToString("F9", CultureInfo.InvariantCulture);
                        dr[10] = ep.Y.ToString("F9", CultureInfo.InvariantCulture);
                        dr[11] = ep.Z.ToString("F9", CultureInfo.InvariantCulture);
                        dt.Rows.Add(dr);
                    }

                    DataRow drlast = dt.NewRow();//마지막에 합계줄도 넣을 수 있는거지
                    drlast[0] = "----------------";
                    drlast[1] = "----------------";
                    drlast[2] = "----------------";
                    drlast[3] = "----------------";
                    drlast[4] = "면적 합계 :";
                    drlast[5] = Math.Round(sum, 3).ToString();
                    for (int k = 6; k <= 11; k++)
                    {
                        drlast[k] = "----------------";
                    }
                    dt.Rows.Add(drlast);

                    dataGridView1.DataSource = dt;

                }

                else if (comboBox5.SelectedItem.ToString() == "Floor")
                {
                    List<string> strs = new List<string>
                    { "No.", "ID", "Level", "Name", "Length", "Area"};
                    DataTable dt = Util.GetDataTableFromStrArr(strs);

                    //DataTable dt = new DataTable();
                    //dt.Columns.Add("No.");
                    //dt.Columns.Add("ID");
                    //dt.Columns.Add("Level");
                    //dt.Columns.Add("Name");
                    //dt.Columns.Add("Length");
                    //dt.Columns.Add("Area");
                    FilteredElementCollector floor_col = new FilteredElementCollector(doc);
                    floor_col.OfCategory(BuiltInCategory.OST_Floors);
                    floor_col.OfClass(typeof(Floor));


                    int i = 0;
                    double sum = 0;

                    foreach (Floor floor in floor_col)
                    {
                        i++;
                        Parameter lenparam = floor.get_Parameter(BuiltInParameter.HOST_PERIMETER_COMPUTED);
                        string len = lenparam.AsValueString();

                        Parameter areaparam = floor.get_Parameter(BuiltInParameter.HOST_AREA_COMPUTED);
                        Double area = Math.Round((areaparam.AsDouble() * 0.09290304), 3);

                        Parameter levparam = floor.get_Parameter(BuiltInParameter.LEVEL_PARAM);
                        string level = levparam.AsValueString();

                        DataRow dr = dt.NewRow();
                        dr[0] = i.ToString();
                        dr[1] = floor.Id.ToString();
                        dr[2] = level;
                        dr[3] = floor.Name;
                        dr[4] = len;
                        dr[5] = area;
                        dt.Rows.Add(dr);

                        sum += area;
                    }

                    DataRow drlast = dt.NewRow();//마지막에 합계줄도 넣을 수 있는거지
                    drlast[0] = "----------------";
                    drlast[1] = "----------------";
                    drlast[2] = "----------------";
                    drlast[3] = "----------------";
                    drlast[4] = "면적의 합계 :";
                    drlast[5] = Math.Round(sum, 3).ToString();

                    dt.Rows.Add(drlast);

                    dataGridView1.DataSource = dt;
                }

                else if (comboBox5.SelectedItem.ToString() == "Column")
                {
                    List<string> strs = new List<string>
                    { "No.", "ID", "Level", "Name", "Length", "Volumn"};
                    DataTable dt = Util.GetDataTableFromStrArr(strs);

                    FilteredElementCollector col_col = new FilteredElementCollector(doc);
                    col_col.OfCategory(BuiltInCategory.OST_StructuralColumns);
                    col_col.OfClass(typeof(FamilyInstance));


                    int i = 0;
                    double sum = 0;

                    foreach (FamilyInstance column in col_col)
                    {
                        i++;
                        Parameter lenparam = column.get_Parameter(BuiltInParameter.INSTANCE_LENGTH_PARAM);
                        string len = lenparam.AsValueString();

                        Parameter volparam = column.get_Parameter(BuiltInParameter.HOST_VOLUME_COMPUTED);//체적
                        Double volumn = Math.Round((volparam.AsDouble() * 0.0283168466), 3);

                        Parameter levparam = column.get_Parameter(BuiltInParameter.FAMILY_BASE_LEVEL_PARAM);
                        string level = levparam.AsValueString();

                        DataRow dr = dt.NewRow();
                        dr[0] = i.ToString();
                        dr[1] = column.Id.ToString();
                        dr[2] = level;
                        dr[3] = column.Name;
                        dr[4] = len;
                        dr[5] = volumn;
                        dt.Rows.Add(dr);

                        sum += volumn;
                    }

                    DataRow drlast = dt.NewRow();//마지막에 합계줄도 넣을 수 있는거지
                    drlast[0] = "----------------";
                    drlast[1] = "----------------";
                    drlast[2] = "----------------";
                    drlast[3] = "----------------";
                    drlast[4] = "체적의 합계 :";
                    drlast[5] = Math.Round(sum, 3).ToString();

                    dt.Rows.Add(drlast);

                    dataGridView1.DataSource = dt;
                }

                else if (comboBox5.SelectedItem.ToString() == "Beam")
                {
                    List<string> strs = new List<string>
                    { "No.", "ID", "Level", "Name", "Length", "Volumn"};
                    DataTable dt = Util.GetDataTableFromStrArr(strs);

                    FilteredElementCollector Beam_col = new FilteredElementCollector(doc);
                    Beam_col.OfCategory(BuiltInCategory.OST_StructuralFraming);
                    Beam_col.OfClass(typeof(FamilyInstance));


                    int i = 0;
                    double sum = 0;

                    foreach (FamilyInstance beam in Beam_col)
                    {
                        i++;
                        Parameter lenparam = beam.get_Parameter(BuiltInParameter.INSTANCE_LENGTH_PARAM);
                        string len = lenparam.AsValueString();

                        Parameter volparam = beam.get_Parameter(BuiltInParameter.HOST_VOLUME_COMPUTED);//체적
                        Double volumn = Math.Round((volparam.AsDouble() * 0.0283168466), 3);

                        Parameter levparam = beam.get_Parameter(BuiltInParameter.INSTANCE_REFERENCE_LEVEL_PARAM);
                        string level = levparam.AsValueString();

                        DataRow dr = dt.NewRow();
                        dr[0] = i.ToString();
                        dr[1] = beam.Id.ToString();
                        dr[2] = level;
                        dr[3] = beam.Name;
                        dr[4] = len;
                        dr[5] = volumn;
                        dt.Rows.Add(dr);

                        sum += volumn;
                    }

                    DataRow drlast = dt.NewRow();//마지막에 합계줄도 넣을 수 있는거지
                    drlast[0] = "----------------";
                    drlast[1] = "----------------";
                    drlast[2] = "----------------";
                    drlast[3] = "----------------";
                    drlast[4] = "체적의 합계 :";
                    drlast[5] = Math.Round(sum, 3).ToString();

                    dt.Rows.Add(drlast);

                    dataGridView1.DataSource = dt;
                }

            });
        }

        private void csv_output_Click(object sender, EventArgs e)//csv파일로 내보내기
        {
            SaveFileDialog csv_save = new SaveFileDialog();
            csv_save.Filter = "CSV 파일|*.csv";//자동으로 csv 파일 만들기

            if (csv_save.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(csv_save.FileName, false, Encoding.UTF8);
                //파일 이름으로 저장하겠다, false = 덮어쓰겠다/true = 밑에다가 얹기, 한글 깨짐 방지(default도 될것)

                List<string> header = new List<string>();
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    header.Add(CsvCell(dataGridView1.Columns[i].HeaderText));
                }
                sw.WriteLine(string.Join(",", header));//Join이라 행 끝에 쉼표가 안 붙음


                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].IsNewRow) continue;

                    //합계 행("-----")은 화면 검산용이라 CSV에서는 제외
                    string first = Convert.ToString(dataGridView1.Rows[i].Cells[0].Value);
                    if (first.StartsWith("---")) continue;

                    List<string> cells = new List<string>();
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)//컬럼 인덱스로 찾아야하니까
                    {
                        cells.Add(CsvCell(dataGridView1.Rows[i].Cells[j].Value));//일단 row 하나 꺼내고, 그 중에 한 셀
                    }
                    sw.WriteLine(string.Join(",", cells));
                }
                sw.Close();//꼭 닫아줘야함
            }
        }

        private static string CsvCell(object value)//쉼표·따옴표·줄바꿈이 있으면 "..."로 감싸 열이 깨지지 않게
        {
            string s = Convert.ToString(value) ?? "";
            if (s.IndexOfAny(new[] { ',', '"', '\n', '\r' }) >= 0)
            {
                s = "\"" + s.Replace("\"", "\"\"") + "\"";
            }
            return s;
        }

        private void CSV_INPUT_Click(object sender, EventArgs e)
        {
            RunRevit((uidoc, doc) =>
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "CSV 파일|*.csv";
                if (ofd.ShowDialog() != DialogResult.OK) return;//취소하면 종료
                string filepath = ofd.FileName;

                FilteredElementCollector levCol = new FilteredElementCollector(doc);
                levCol.OfClass(typeof(Level));
                List<Level> levels = levCol.Cast<Level>().ToList();
                int skipped = 0;

                string[] strs = File.ReadAllLines(filepath, Encoding.UTF8);
                foreach (string str in strs)
                {
                    string[] spSTR = str.Split(",");

                    //좌표(12열 이상)가 없는 줄(헤더, 빈줄, 내보내기 CSV)은 건너뜀
                    if (spSTR.Length < 12 || !double.TryParse(spSTR[6].Trim('(', ')', ' '), NumberStyles.Float, CultureInfo.InvariantCulture, out _))
                    {
                        skipped++;
                        continue;
                    }

                    string walltype = spSTR[3];

                    string spx = spSTR[6];
                    string spy = spSTR[7];
                    string spz = spSTR[8];

                    string epx = spSTR[9];
                    string epy = spSTR[10];
                    string epz = spSTR[11];

                    double sp_x = double.Parse(spx.Trim('(', ')', ' '), CultureInfo.InvariantCulture);
                    double sp_y = double.Parse(spy.Trim('(', ')', ' '), CultureInfo.InvariantCulture);
                    double sp_z = double.Parse(spz.Trim('(', ')', ' '), CultureInfo.InvariantCulture);

                    double ep_x = double.Parse(epx.Trim('(', ')', ' '), CultureInfo.InvariantCulture);
                    double ep_y = double.Parse(epy.Trim('(', ')', ' '), CultureInfo.InvariantCulture);
                    double ep_z = double.Parse(epz.Trim('(', ')', ' '), CultureInfo.InvariantCulture);

                    XYZ sp = new XYZ(sp_x, sp_y, sp_z);
                    XYZ ep = new XYZ(ep_x, ep_y, ep_z);

                    Line line = Line.CreateBound(sp, ep);
                    WallType wt = Util.FindWallTypeByName(doc, walltype);
                    if (wt == null) { skipped++; continue; }

                    //3D뷰는 GenLevel이 null → CSV의 레벨명, 없으면 z값에 가장 가까운 레벨
                    Level lev = levels.FirstOrDefault(l => l.Name == spSTR[2].Trim())
                        ?? levels.OrderBy(l => Math.Abs(l.Elevation - sp_z)).FirstOrDefault();
                    if (lev == null) { skipped++; continue; }

                    using (Transaction trans = new Transaction(doc, "test"))
                    {
                        trans.Start();
                        Wall wall = Wall.Create(doc, line, wt.Id, lev.Id, 3000 / 304.8, 0, false, true);
                        trans.Commit();
                    }

                    //xyz도 , 구분으로는 x값만 나와버리니까, 3개씩 묶던가, @로 구분자를 바꾸던가
                    //TaskDialog.Show("데이터확인", walltype + "_" + sp + "_" + ep);
                }

            });

        }

        private void Level_Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_BottomLevelSTR = Level_Picker.SelectedItem.ToString();
        }

        private void Level_Top_Pick_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_TopLevelSTR = Level_Top_Pick.SelectedItem.ToString();
        }

        private void CreateRoom_Click(object sender, EventArgs e)
        {
            RunRevit((uidoc, doc) =>
            {
                IList<Reference> refs = uidoc.Selection.PickObjects(ObjectType.Element, new RoomFilter(), "룸을 선택하세요");

                foreach (Reference r in refs)
                {
                    Room room = (Room)doc.GetElement(r);//as랑 같은게 앞쪽 괄호

                    //<룸의 sl 값을 찾는다>

                    //3D 뷰를 일단 찾는다 - 총만들기에 필요해서
                    FilteredElementCollector col = new FilteredElementCollector(doc);
                    col.OfClass(typeof(View3D));
                    View3D view3D = null;
                    foreach (View3D v in col)
                    {
                        if (v.IsTemplate == false)
                        {
                            view3D = v;
                            break;
                        }
                    }

                    if (view3D == null)//예외처리
                    {
                        TaskDialog.Show("Error", "3D 뷰가 없습니다");
                        return;
                    }

                    ElementCategoryFilter floor_filter = new ElementCategoryFilter(BuiltInCategory.OST_Floors);

                    ReferenceIntersector ri = new ReferenceIntersector(floor_filter, FindReferenceTarget.Element, view3D);
                    //element or face 가져오면 됨. 총만 만들고 아직 안 쏜 상태

                    LocationPoint lp = room.Location as LocationPoint;
                    XYZ sp = new XYZ(lp.Point.X, lp.Point.Y, lp.Point.Z + 1200 / 304.8);
                    //룸이 너무 바닥에 붙으면 못찾을수도 있으니까 z값만 좀 올려서

                    ReferenceWithContext rc = ri.FindNearest(sp, -XYZ.BasisZ);
                    //총 쏘는거! 아래로 쏴야 슬라브를 찾으니까.

                    double slz = 0;
                    if (rc != null)
                    {
                        Reference rsl = rc.GetReference();//걸린 놈 가져오기
                        XYZ hit = rsl.GlobalPoint;//히팅포인트를 xyz로 치환해서 가져오기
                        slz = hit.Z;//z값만 뽑아내기
                    }

                    SpatialElementBoundaryOptions boundaryOptions = new SpatialElementBoundaryOptions();
                    boundaryOptions.SpatialElementBoundaryLocation = SpatialElementBoundaryLocation.Finish;//안쪽 마감선을 바운더리라고 지정

                    IList<IList<BoundarySegment>> loops = room.GetBoundarySegments(boundaryOptions);

                    List<CurveLoop> cls = new List<CurveLoop>();
                    foreach (IList<BoundarySegment> loop in loops)
                    {
                        CurveLoop cl = new CurveLoop();
                        foreach (BoundarySegment bs in loop)
                        {
                            Curve c = bs.GetCurve();
                            cl.Append(c);
                        }
                        cls.Add(cl);
                    }

                    Level lv = Util.GetLevelByName(doc, m_BottomLevelSTR);
                    if (lv == null)//lv가 없는 경우 예외처리
                    {
                        TaskDialog.Show("Error", "레벨이 없습니다");
                        return;
                    }

                    double floorthk = 0;

                    if (m_floor_checked == true)
                    {
                        //바닥을 생성한다
                        FloorType ft = Util.FindFloorTypeByName(doc, m_floortypename);
                        if (ft == null)//ft가 없는 경우 예외처리
                        {
                            TaskDialog.Show("Error", "바닥 타입이 없습니다");
                            return;
                        }

                        Parameter param_THK = ft.get_Parameter(BuiltInParameter.FLOOR_ATTR_DEFAULT_THICKNESS_PARAM);
                        if (param_THK == null)
                        {
                            TaskDialog.Show("Error", "두께 값이 없습니다");
                            return;
                        }

                        floorthk = param_THK.AsDouble();

                        using (Transaction trans = new Transaction(doc, "바닥을 생성합니다"))
                        {
                            trans.Start();
                            Floor f = Floor.Create(doc, cls, ft.Id, lv.Id);
                            Parameter Upparam = f.get_Parameter(BuiltInParameter.FLOOR_HEIGHTABOVELEVEL_PARAM);
                            if (Upparam != null)//값이 있는 경우에만~
                            {
                                Upparam.Set(floorthk + slz);
                            }
                            trans.Commit();
                        }
                    }

                    double w_thk = 0;
                    if (m_wall_checked == true)
                    {
                        //벽 그리기
                        WallType wt = Util.FindWallTypeByName(doc, m_walltypename);
                        if (wt == null)
                        {
                            TaskDialog.Show("Error", "벽 유형이 없습니다.");
                            return;
                        }
                        double wallheight = Convert.ToDouble(m_wallheight);
                        Parameter wallthk = wt.get_Parameter(BuiltInParameter.WALL_ATTR_WIDTH_PARAM);
                        w_thk = wallthk.AsDouble();

                        foreach (CurveLoop cl in cls)
                        {
                            CurveLoop offloop = CurveLoop.CreateViaOffset(cl, -w_thk / 2, XYZ.BasisZ);

                            foreach (Curve c in offloop)
                            {
                                using (Transaction trans = new Transaction(doc, "벽을 생성하겠습니다."))
                                {
                                    trans.Start();
                                    Wall wall = Wall.Create(doc, c, wt.Id, lv.Id, wallheight / 304.8, floorthk + slz, false, false);
                                    trans.Commit();
                                }

                            }
                        }

                    }

                    if (m_ceiling_checked == true)
                    {
                        CeilingType ct = Util.FindCeilingTypeByName(doc, m_ceilingtypename);

                        double ceilingheight = Convert.ToDouble(m_ceilingheight);
                        List<CurveLoop> offloop = new List<CurveLoop>();
                        foreach (CurveLoop cl in cls)
                        {
                            if (w_thk > 0)
                            {
                                CurveLoop ocl = CurveLoop.CreateViaOffset(cl, -w_thk, XYZ.BasisZ);
                                offloop.Add(ocl);
                            }
                            else { offloop.Add(cl); }
                        }

                        using (Transaction trans = new Transaction(doc, "천장을 생성합니다"))
                        {
                            trans.Start();
                            Ceiling c = Ceiling.Create(doc, offloop, ct.Id, lv.Id);
                            Parameter Ceilparam = c.get_Parameter(BuiltInParameter.CEILING_HEIGHTABOVELEVEL_PARAM);
                            if (Ceilparam != null)//값이 있는 경우에만~
                            {
                                Ceilparam.Set(ceilingheight / 304.8 + floorthk + slz);
                            }
                            trans.Commit();
                        }


                    }
                }
            });
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                m_floor_checked = true;
            }
            else
            {
                m_floor_checked = false;
            }
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                m_wall_checked = true;
            }
            else
            {
                m_wall_checked = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                m_ceiling_checked = true;
            }
            else
            {
                m_ceiling_checked = false;
            }
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)//천장타입
        {
            m_ceilingtypename = comboBox6.SelectedItem.ToString();
        }
        private void textBox2_TextChanged(object sender, EventArgs e)//천장높이
        {
            m_ceilingheight = textBox2.Text;
        }

        private void CreateDoor_Click(object sender, EventArgs e)
        {
            RunRevit((uidoc, doc) =>
            {
                IList<Reference> refs = uidoc.Selection.PickObjects(ObjectType.Element, "디테일 라인을 선택하세요");
                List<DetailCurve> dec = new List<DetailCurve>();
                foreach (Reference r in refs)
                {
                    DetailCurve dc = doc.GetElement(r) as DetailCurve;
                    dec.Add(dc);
                }

                Dictionary<Wall, Curve> dix = new Dictionary<Wall, Curve>();

                FilteredElementCollector col = new FilteredElementCollector(doc);
                col.OfClass(typeof(Wall));

                foreach (Wall w in col)
                {
                    LocationCurve lc = w.Location as LocationCurve;
                    Curve c = lc.Curve;
                    dix.Add(w, c);
                }

                //문 만드는 과정 시작
                foreach (DetailCurve dc in dec)
                {
                    IntersectionResultArray ira;//결과값의 ARRAY
                    Curve c1 = dc.GeometryCurve;
                    Wall hostwall = null;
                    XYZ p1 = null;
                    foreach (KeyValuePair<Wall, Curve> dic in dix)
                    {
                        SetComparisonResult sr = c1.Intersect(dic.Value, out ira);//out 쓰면 결과 받아서 바로 담기 가능
                        //비교하는거

                        if (ira != null) //0이면 넘어가지 말고 위로 가서 다시 검색해라, 교점 생기면 들어와라
                        {
                            p1 = ira.get_Item(0).XYZPoint;//여러개일수도 있지만 일단 0번째 포인트를 xyz 값으로 줘라
                            hostwall = dic.Key;
                        }
                    }

                    if (hostwall == null || p1 == null)// 또는 연산자
                    {
                        TaskDialog.Show("Error", "벽과 만나는 점이 없습니다");
                        return;
                    }


                    FamilySymbol fs = Util.GetDoorTypeByName(doc, m_doortypename);
                    if (fs == null)
                    {
                        TaskDialog.Show("Error", "문 타입이 없습니다");
                        return;
                    }

                    using (Transaction tran = new Transaction(doc, "문을 생성합니다"))
                    {
                        tran.Start();
                        fs.Activate();
                        FamilyInstance door = doc.Create.NewFamilyInstance(p1, fs, hostwall, StructuralType.NonStructural);
                        tran.Commit();
                    }

                }




            });

        }
        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)//문 타입
        {
            m_doortypename = comboBox7.SelectedItem.ToString();
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)//보 타입
        {
            m_beamtypename = comboBox2.SelectedItem.ToString();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)//기둥 타입
        {
            m_coltypename = comboBox3.SelectedItem.ToString();
        }

        private void CreateBeam_Click(object sender, EventArgs e)//보 생성
        {
            RunRevit((uidoc, doc) =>
            {
                Reference r = uidoc.Selection.PickObject(ObjectType.Face);
                Element e = doc.GetElement(r);
                Face f = e.GetGeometryObjectFromReference(r) as Face;

                List<Curve> c = Util.GetCurvesFromFace(f);
                FamilySymbol fs = Util.GetFamilySymbolByName(m_beamtypename, doc);

                if (fs == null)
                {
                    TaskDialog.Show("Error", "보 타입이 없습니다.");
                    return;
                }
                Level lv = Util.GetLevelByName(doc, m_BottomLevelSTR);
                if (lv == null)//lv가 없는 경우 예외처리
                {
                    TaskDialog.Show("Error", "하단 레벨이 없습니다");
                    return;
                }

                Util.CreateFamilyInstanceFromCurve(c,fs,lv,doc);
            });
        }

        private void CreateCol_Click(object sender, EventArgs e)//기둥 생성
        {
            RunRevit((uidoc, doc) =>
            {
                IList<Reference> refs = uidoc.Selection.PickObjects(ObjectType.Face);
                List<Curve> c = new List<Curve>();
                foreach (Reference r in refs)
                {
                    Element e = doc.GetElement(r);
                    Face f = e.GetGeometryObjectFromReference(r) as Face;
                    EdgeArrayArray edgeArrays = f.EdgeLoops;

                    foreach (EdgeArray edgearray in edgeArrays)
                    {
                        foreach (Edge edge in edgearray)
                        {
                            Curve C = edge.AsCurve();
                            c.Add(C);
                        }
                    }
                }


                FilteredElementCollector col = new FilteredElementCollector(doc);
                col.OfCategory(BuiltInCategory.OST_StructuralColumns);
                col.OfClass(typeof(FamilySymbol));
                FamilySymbol fs = null;

                foreach (FamilySymbol symbol in col)
                {
                    if (m_coltypename == symbol.Name)
                    {
                        fs = symbol;
                        break;
                    }
                }

                if (fs == null)
                {
                    TaskDialog.Show("Error", "기둥 타입이 없습니다.");
                    return;
                }


                Level lvBot = Util.GetLevelByName(doc, m_BottomLevelSTR);
                Level lvTop = Util.GetLevelByName(doc, m_TopLevelSTR);

                if (lvBot == null || lvTop == null)
                {
                    TaskDialog.Show("Error", "하단/상단 레벨을 모두 선택하세요.");
                    return;
                }
                if (lvTop.Elevation <= lvBot.Elevation)
                {
                    TaskDialog.Show("Error", "상단 레벨이 하단 레벨보다 높아야 합니다.");
                    return;
                }

                using (Transaction trans = new Transaction(doc, "Create Column"))
                {
                    trans.Start();
                    fs.Activate();

                    foreach (Curve c1 in c)
                    {
                        XYZ p = c1.GetEndPoint(0);                       // 모서리 점
                        XYZ pt = new XYZ(p.X, p.Y, lvBot.Elevation);     // Z는 하단 레벨 높이로

                        FamilyInstance fi = doc.Create.NewFamilyInstance(pt, fs, lvBot, StructuralType.Column);
                        fi.get_Parameter(BuiltInParameter.FAMILY_TOP_LEVEL_PARAM).Set(lvTop.Id);   // 상단 레벨
                    }
                    trans.Commit();
                }
            });
        }


        // ───────────── 아래는 수정할 필요 없습니다 ─────────────

        /// <summary>
        /// 번호표를 뽑습니다. { } 안의 할 일은 내 차례가 되면 실행됩니다.
        /// </summary>
        private void RunRevit(Action<UIDocument, Document> action)
        {
            // 번호표에 할 일을 적고
            _action = action;
            // 번호표 뽑기 (바로 실행 X → 차례가 되면 Revit 이 Execute() 호출)
            // ※ 차례가 오기 전에 다시 누르면, 마지막에 누른 버튼의 할 일만 실행됩니다.
            _exEvent.Raise();
        }

        /// <summary>
        /// 내 차례! Revit 이 호출해 줍니다. 번호표에 적어 둔 할 일을 실행합니다.
        /// 이 안에서는 Revit API 를 자유롭게 사용할 수 있습니다.
        /// </summary>
        public void Execute(UIApplication app)
        {
            if (_action == null) return;

            UIDocument uidoc = app.ActiveUIDocument;
            if (uidoc == null)
            {
                TaskDialog.Show("Modless", "열려 있는 문서가 없습니다.");
                return;
            }

            try
            {
                _action(uidoc, uidoc.Document);
            }
            catch (Exception ex)
            {
                TaskDialog.Show("오류", ex.Message);
            }
            finally
            {
                _action = null;
            }
        }

        /// <summary>
        /// 핸들러 이름. (IExternalEventHandler)
        /// Revit 이 내부적으로 이벤트를 구분할 때 사용합니다. 아무 이름이나 괜찮습니다.
        /// </summary>
        public string GetName()
        {
            return "Modless";
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            // 번호표 기계 철거
            _exEvent.Dispose();
            base.OnFormClosed(e);
        }

    }

    public class RoomFilter : ISelectionFilter
    {
        public bool AllowElement(Element elem)
        {
            return elem is Room;
        }

        public bool AllowReference(Reference reference, XYZ position)//room 아니면 선택도 아예 안되게 하는 것
        {
            return false;
        }
    }
}
