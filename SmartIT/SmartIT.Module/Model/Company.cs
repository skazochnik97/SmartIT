using System;
using LinqToDB.Mapping;

namespace SmartIT.Module.Model
{

    [Table(Name = "Company")]
    public class Company : BaseObject
    {

        [Column(Name = "ShortName")]
        public string ShortName { get; set; }

 
        private DateTime? dateCreate;
        [Column(Name = "DateCreate")]
        public DateTime? DateCreate
        {
            get
            {
                return dateCreate;
            }
            set
            {
                dateCreate = value;
            }
        }

   
        private DateTime? dateCancel;
        [Column(Name = "DateCancel")]
        public DateTime? DateCancel
        {
            get
            {
                return dateCancel;
            }
            set
            {
                dateCancel = value;
            }
        }

    }
}