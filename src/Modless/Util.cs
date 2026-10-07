using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.Data;


namespace Modless
{
    public class Util
    {
        /// <summary>
        /// 선택한 Face의 Edge를 Curve로 반환하는 함수
        /// </summary>
        /// <param name="Face from Revit"></param>
        /// <returns></returns>
        public static List<Curve> GetCurvesFromFace(Face face)
        {
            List<Curve> curves = new List<Curve>();
            EdgeArrayArray edgeArrays = face.EdgeLoops;//face 터트려서 edge 가져오기. list 안에 list 구조.. graft구조..

            foreach (EdgeArray edgearray in edgeArrays)
            {
                foreach (Edge edge in edgearray)
                {
                    Curve C = edge.AsCurve();
                    curves.Add(C);
                }
            }

            return curves;
        }

        /// <summary>
        /// 이름으로 빔타입(패밀리심볼) 찾기
        /// </summary>
        /// <param name="FamilySymbol Name"></param>
        /// <param name="Revit File"></param>
        /// <returns></returns>
        public static FamilySymbol GetFamilySymbolByName(string name, Document doc)
        {
            FilteredElementCollector col = new FilteredElementCollector(doc);
            col.OfCategory(BuiltInCategory.OST_StructuralFraming);
            col.OfClass(typeof(FamilySymbol));
            FamilySymbol fs = null;

            foreach (FamilySymbol symbol in col)
            {
                if (name == symbol.Name)
                {
                    fs = symbol;
                    break;
                }
            }

            return fs;
        }

        /// <summary>
        /// XYZ 좌표 리스트로 Curve 리스트를 반환한다
        /// </summary>
        /// <param name="XYZ pts"></param>
        /// <returns></returns>
        public static List<Curve> GetCurveListFromPts(List<XYZ> pts)
        {
            List<Curve> curves = new List<Curve>();

            for (int i = 0; i < pts.Count - 1; i++)
            {
                Line line = Line.CreateBound(pts[i], pts[i + 1]);
                curves.Add(line);
            }


            return curves;
        }

        /// <summary>
        /// 커브 리스트로 보를 만든다
        /// </summary>
        /// <param name="curves"></param>
        /// <param name="fs"></param>
        /// <param name="level"></param>
        /// <param name="doc"></param>
        public static void CreateFamilyInstanceFromCurve(List<Curve> curves, FamilySymbol fs, Level level, Document doc)
        //결과값을 받지 않을 때 void
        {
            using (Transaction trans = new Transaction(doc, "Create Beam"))
            {
                trans.Start();
                fs.Activate();
                foreach (Curve curve in curves)
                    doc.Create.NewFamilyInstance(curve, fs, level, StructuralType.Beam);
                trans.Commit();
            }
        }

        /// <summary>
        /// XYZ 좌표 리스트로 CurveLoop를 반환한다
        /// </summary>
        /// <param name="XYZ Pts"></param>
        /// <returns></returns>
        public static CurveLoop GetCurveLoopFromPts(List<XYZ> points)
        {
            CurveLoop cl = new CurveLoop();

            for (int i = 0; i < points.Count; i++)
            {
                if (i < points.Count - 1)
                {
                    Line line = Line.CreateBound(points[i], points[i + 1]);
                    cl.Append(line);
                }

                else if (i == points.Count - 1)
                {
                    Line line = Line.CreateBound(points[i], points[0]);
                    cl.Append(line);
                }
            }
            return cl;
        }

        /// <summary>
        /// 커브루프로 바닥 슬라브 만들기
        /// </summary>
        /// <param name="Revit doc"></param>
        /// <param name="CurveLoop"></param>
        /// <param name="FloorID"></param>
        /// <param name="LevelID"></param>
        /// <param name="thickness"></param>
        public static void CreateFloor(Document doc, IList<CurveLoop> cl, ElementId FloorID, ElementId LevelID, double thickness)
        {
            using (Transaction trans = new Transaction(doc, "바닥을 생성합니다"))
            {
                trans.Start();

                Floor F = Floor.Create(doc, cl, FloorID, LevelID);

                Parameter param = F.get_Parameter(BuiltInParameter.FLOOR_HEIGHTABOVELEVEL_PARAM);
                param.Set(thickness);

                trans.Commit();
            }
        }

        /// <summary>
        /// 이름으로 FloorType 만들기
        /// </summary>
        /// <param name="Revit doc"></param>
        /// <param name="이름"></param>
        /// <returns></returns>
        public static FloorType FindFloorTypeByName(Document doc, string name)
        {
            FilteredElementCollector col = new FilteredElementCollector(doc);
            col.OfCategory(BuiltInCategory.OST_Floors);
            col.OfClass(typeof(FloorType));//instance말고 type만 찾는구나 확인

            FloorType ft = null;

            foreach (FloorType floortype in col)
            {
                if (name == floortype.Name)
                {
                    ft = floortype;
                    break;
                }
            }
            return ft;
        }

        /// <summary>
        /// 이름으로 Wall Type 만들기
        /// </summary>
        /// <param name="Revit doc"></param>
        /// <param name="이름"></param>
        /// <returns></returns>
        public static WallType FindWallTypeByName(Document doc, string name)
        {
            FilteredElementCollector col = new FilteredElementCollector(doc);
            col.OfCategory(BuiltInCategory.OST_Walls);
            col.OfClass(typeof(WallType));
            WallType wt = null;

            foreach (WallType Walltype in col)
            {
                if (name == Walltype.Name)
                {
                    wt = Walltype;
                    break;
                }
            }

            //다른 버전!
            //FilteredElementCollector col1 = new FilteredElementCollector(doc);
            //col1.OfCategory(BuiltInCategory.OST_Walls);
            //col1.OfClass(typeof(WallType)).FirstOrDefault(q => q.Name == name);//foreach문 대신 간단하게

            //또 다른 버전
            //WallType walltype = new FilteredElementCollector(doc).OfClass(typeof(WallType))
            //   .Cast<WallType>().FirstOrDefault(q => q.Name == name);//아예 한줄로 다 가능. 편하지만 오류대처는 힘듦
            //캐스트가 as~~의 최신버전 / 객체 중 무명 method 사용해서 이름 같은거 캐스트 해줘라

            return wt;
        }

        /// <summary>
        /// 벽을 만든다
        /// </summary>
        /// <param name="Revit doc"></param>
        /// <param name="curve"></param>
        /// <param name="Wall Type"></param>
        /// <param name="level"></param>
        /// <param name="벽 높이(변환없이 그대로 넣기)"></param>
        public static void CreateWall(Document doc, Curve curve, WallType wt, Level level, double height, bool structure)
        {
            using (Transaction trans = new Transaction(doc, "마감벽을 그립니다."))
            {
                trans.Start();

                Wall wall = Wall.Create(doc, curve, wt.Id, level.Id, height / 304.8, 0, false, structure);
                //여기서 offset은 레벨로부터 띄우기 값

                trans.Commit();
            }

        }

        /// <summary>
        /// 벽체 두께 알아내기
        /// </summary>
        /// <param name="Wall Type"></param>
        /// <returns></returns>
        public static double GetWallTHK(WallType wt)
        {
            Parameter param = wt.get_Parameter(BuiltInParameter.WALL_ATTR_WIDTH_PARAM);
            double thk = param.AsDouble();//안쪽 마감 offset

            return thk;
        }

        /// <summary>
        /// 데이터 테이블 만들기
        /// </summary>
        /// <param name="list_string"></param>
        /// <returns></returns>
        public static DataTable GetDataTableFromStrArr(List<string> str)
        {
            DataTable dt = new DataTable();

            foreach (string s in str)
            {
                dt.Columns.Add(s);
            }
            return dt;
        }

        /// <summary>
        /// 레벨 찾기
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Level GetLevelByName(Document doc, string name)
        {
            Level findLevel = null;

            FilteredElementCollector col = new FilteredElementCollector(doc);
            col.OfClass(typeof(Level));

            foreach (Level lv in col)
            {
                if (lv.Name == name)
                {
                    findLevel = lv;
                    break;
                }
            }
            return findLevel;
        }

        /// <summary>
        /// 천장 타입 찾기
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static CeilingType FindCeilingTypeByName(Document doc, string name)
        {
            FilteredElementCollector col = new FilteredElementCollector(doc);
            col.OfCategory(BuiltInCategory.OST_Ceilings);
            col.OfClass(typeof(CeilingType));
            CeilingType ct = null;

            foreach (CeilingType Ceilingtype in col)
            {
                if (name == Ceilingtype.Name)
                {
                    ct = Ceilingtype;
                    break;
                }
            }
            return ct;
        }

        /// <summary>
        /// 문 타입 찾는다
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static FamilySymbol GetDoorTypeByName(Document doc, string name)
        {
            FilteredElementCollector col = new FilteredElementCollector(doc);
            col.OfCategory(BuiltInCategory.OST_Doors);
            col.OfClass(typeof(FamilySymbol));
            FamilySymbol door = null;

            foreach (FamilySymbol symbol in col)
            {
                if (name == symbol.Name)
                {
                    door = symbol;
                    break;
                }
            }

            return door;
        }


    }
}