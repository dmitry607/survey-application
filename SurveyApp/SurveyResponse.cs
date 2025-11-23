using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp
{
    public class SurveyResponse
    {
        public int id {  get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public int TotalScore { get; set; }

    }
}
