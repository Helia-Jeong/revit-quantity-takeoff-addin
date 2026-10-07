using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Modless;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Modless
{
    public class FloorATT//바닥만드는거 총집합
    {
        public static List<FloorData> GetFloorData(Document doc, UIDocument uiDoc, string m_floortypename) //우리가 만든 객체 그 자체를 받을 수 있음
        {
            List<FloorData> floors = new List<FloorData>();

            IList<Reference> refs = uiDoc.Selection.PickObjects(ObjectType.Face, "구조 바닥을 선택하세요.");//face 선택해서 슬라브 그리기

            Element e1 = doc.GetElement(refs[0]);
            Floor levelfloor = e1 as Floor;//floor로 일단 변형시키기

            Parameter levelparam = levelfloor.get_Parameter(BuiltInParameter.LEVEL_PARAM);
            ElementId level = levelparam.AsElementId();


            foreach (Reference r in refs)
            {
                FloorData fclass = new FloorData();
                fclass.m_level = level;

                Face f = doc.GetElement(r).GetGeometryObjectFromReference(r) as Face;//일단 가져온 다음 face로 변환
                EdgeArrayArray eaa = f.EdgeLoops;//face에서 edgeloop를 가져오기
                IList<CurveLoop> CLS = new List<CurveLoop>();

                foreach (EdgeArray ea in eaa)//arrayarray에서 array빼내기
                {
                    CurveLoop cl = new CurveLoop();

                    foreach (Edge eg in ea)//array에서 edge빼내기
                    {
                        Curve c = eg.AsCurveFollowingFace(f);//edge의 방향성을 지키기 위해 사용. face를 따라다니면서 커브 만들기
                        cl.Append(c);
                    }
                    CLS.Add(cl);
                }
                fclass.m_CurveLoops = CLS;

                FloorType ft = Util.FindFloorTypeByName(doc, m_floortypename);
                fclass.m_FloorType = ft.Id;

                Parameter param = ft.get_Parameter(BuiltInParameter.FLOOR_ATTR_DEFAULT_THICKNESS_PARAM);//두께찾기
                if (param == null)
                {
                    TaskDialog.Show("경고", "두께 값이 없습니다.");
                }

                double t = param.AsDouble();//두께 가져왔다
                fclass.m_FloorTypeTHK = t;

                floors.Add(fclass);
            }

            return floors;
        }
    }


    public class FloorData
    {
        public ElementId m_level { get; set; }

        public ElementId m_FloorType { get; set; }

        public IList<CurveLoop> m_CurveLoops { get; set; }

        public double m_FloorTypeTHK { get; set; }


    }

}
