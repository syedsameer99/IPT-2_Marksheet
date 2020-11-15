using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using Newtonsoft.Json;


namespace MarkSheet
{
    /// <summary>
    /// Summary description for WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {
        //public static List<StudentModel> students = new List<StudentModel>();



        //[WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]

        //public string GetStudents()
        //{
        //    string str = JsonConvert.SerializeObject(students);

        //    return str;
        //}

        //[WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        //public void AddStudent()
        //{
        //    string studentStr = HttpContext.Current.Request.Params["request"];

        //    Subject student = JsonConvert.DeserializeObject<Subject>(studentStr);

        //    students.Add(student);

        //}
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public string Calculate()
        {
            string data = HttpContext.Current.Request["request"];
            Subject[] Subjects = JsonConvert.DeserializeObject<Subject[]>(data);

            Subject MinSubjects = Subjects.First(x => x.marks == Subjects.Min(y => y.marks));
            Subject MaxSubjects = Subjects.First(x => x.marks == Subjects.Max(y => y.marks));

            decimal noOfSubjects = Subjects.Count();
            decimal totalMarks = noOfSubjects * 100;
            decimal totalmarks = Subjects.Sum(x => x.marks);
            decimal percent = (totalmarks / totalMarks) * 100;

            Result result = new Result()
            {
                MinSubject = MinSubjects,
                MaxSubject = MaxSubjects,
                Percentage = percent
            };

            return JsonConvert.SerializeObject(result);
        }

        public class Subject
        {
            public string name { get; set; }
            public int marks { get; set; }

        }

        public class Result
        {
            public Subject MinSubject { get; set; }
            public Subject MaxSubject { get; set; }
            public decimal Percentage { get; set; }
        }
    }
    internal class StripMethodAttribute : Attribute
    {
        private ResponseFormat json;
        private bool UseHttpGet;

        public StripMethodAttribute(ResponseFormat json, bool UseHttpGet)
        {
            this.json = json;
            this.UseHttpGet = UseHttpGet;
        }
    }
}
