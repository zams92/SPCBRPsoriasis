using DevExpress.Xpo;
using DevExpress.ExpressApp;
using DevExpress.Xpo.DB;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ReportsV2;
using SPCBRPsoriasis.Module.BusinessObjects.Menu.Pasien;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;

namespace SPCBRPsoriasis.Module.BusinessObjects.Menu.Laporan
{
    [DomainComponent]
    public class ParameterHasilDiagnosa :ReportParametersObjectBase
    {
    [ModelDefault("AllowEdit", "false")]
    public bool SortByKodeKasus { get; set; }
    [ModelDefault("AllowEdit", "false")]
    public bool Filterbykodepasien { get; set; }
    public DataPasien KodePasien { get; set; }

    public ParameterHasilDiagnosa(IObjectSpaceCreator provider)
        : base(provider)
    {
        SortByKodeKasus = true;
        Filterbykodepasien = true;
        string awalhuruf = SecuritySystem.CurrentUserName;
        if (awalhuruf.Substring(0, 1) == "P")
        {
            KodePasien = null;
            KodePasien = ObjectSpace.FindObject<DataPasien>(new BinaryOperator("kodepasien", awalhuruf));
        }
    }

    protected override IObjectSpace CreateObjectSpace() {
        return objectSpaceCreator.CreateObjectSpace(typeof(Kedekatan));
    }

    public override CriteriaOperator GetCriteria() {
        CriteriaOperator criteriaKodepasien = null;
        if (Filterbykodepasien) {
            criteriaKodepasien = CriteriaOperator.Parse("datapasien = ?", KodePasien);
        }
        return CriteriaOperator.And(criteriaKodepasien);
    }

    public override SortProperty[] GetSorting() {
        List<SortProperty> sorting = new List<SortProperty>();
        if (SortByKodeKasus) {
            sorting.Add(new SortProperty("datakasus.kodekasus", SortingDirection.Ascending));
        }
        return sorting.ToArray();
    }

    public override string ToString() {
        return ".....";
    }
    }
}
