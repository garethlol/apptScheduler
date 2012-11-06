using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDT_A1_s3252820
{
    abstract class Person
    {
        private string name;
        private string id;
        private string email;

        

        public Person(string name, string id, string email)
        {
            this.name = name;
            this.id = id;
            this.email = email;
        }

        public string Name
        {
            get{
                return name;   
            }
            set{
                name = value;   
            }
        }

        public string ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
            }
        }

    }
}
