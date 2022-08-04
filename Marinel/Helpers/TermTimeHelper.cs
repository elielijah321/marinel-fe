using SchoolDraftWebsite.Data.Entities;
using SchoolDraftWebsite.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolDraftWebsite.Helpers
{
    public static class TermTimeHelper
    {
        private static int TermOneStartMonth = 9;
        private static int TermOneEndMonth = 12;

        private static int TermTwoStartMonth = 1;
        private static int TermTwoEndMonth = 4;

        private static int TermThreeStartMonth = 5;
        private static int TermThreeEndMonth = 8;

        public static List<FeedingInfoItem> CalculateTerm(List<FeedingInfoItem> feedingInfoItems)
        {
            return feedingInfoItems.Select(fi => {
                fi.Term = CalculateTerm(fi.Date);
                return fi;
            }).ToList();
        }

        public static List<ClassFeeInfoItem> CalculateTerm(List<ClassFeeInfoItem> classFeeInfoItems)
        {
            return classFeeInfoItems.Select(cf => {
                cf.Term = CalculateTerm(cf.Date);
                return cf;
            }).ToList();
        }

        public static Term CalculateTerm(DateTime date)
        {
            var month = date.Month;

            Term term;

            if (month >= TermOneStartMonth && month <= TermOneEndMonth)
            {
                term = Term.TermOne;
            }
            else if(month >= TermTwoStartMonth && month <= TermTwoEndMonth)
            {
                term = Term.TermTwo;
            }
            else
            {
                term = Term.TermThree;
            }

            return term;
        }
    }
}
