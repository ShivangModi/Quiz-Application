//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuizApplication.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TBL_Question
    {
        public long questionId { get; set; }
        public Nullable<long> questionExamId { get; set; }
        public string questionText { get; set; }
        public string questionOption1 { get; set; }
        public string questionOption2 { get; set; }
        public string questionOption3 { get; set; }
        public string questionOption4 { get; set; }
        public string questionAnswer { get; set; }
        public Nullable<int> questionType { get; set; }
    
        public virtual TBL_Exam TBL_Exam { get; set; }
    }
}
