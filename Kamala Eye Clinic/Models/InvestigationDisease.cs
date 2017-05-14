using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kamala_Eye_Clinic.Models
{
    public class InvestigationDisease
    {
        public int invdisid { get; set; }
        public string invdisname { get; set; }
        public bool ischeked { get; set; }

        public List<InvestigationDisease> InvestigationList()
        {
            return new List<InvestigationDisease>()
            {
                new InvestigationDisease() {invdisid=1,invdisname="CBC",ischeked=false },
                new InvestigationDisease() {invdisid=2,invdisname="Hiv",ischeked=false },
                new InvestigationDisease() {invdisid=3,invdisname="Urine Comp",ischeked=false },
                new InvestigationDisease() {invdisid=4,invdisname="R.B.Sugar",ischeked=false },
                new InvestigationDisease() {invdisid=5,invdisname="HBsAg Disease",ischeked=false },
                new InvestigationDisease() {invdisid=6,invdisname="ESR",ischeked=false },
                new InvestigationDisease() {invdisid=7,invdisname="MT",ischeked=false },
                new InvestigationDisease() {invdisid=8,invdisname="R.A.Factor",ischeked=false },
                new InvestigationDisease() {invdisid=9,invdisname="Fitness Certificate",ischeked=false },
                new InvestigationDisease() {invdisid=10,invdisname="A/Scan",ischeked=false },
            };
        }
    }
}