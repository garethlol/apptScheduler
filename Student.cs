using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDT_A1_s3252820
{
    class Student : Person
    {
        public Student(string name, string id, string email) :
            base(name, id, email){}

        public void viewFreeSlot(string staffName)
        {

        }

        public bool makeBooking()
        {
            return false;
        }

        public void deleteBooking()
        {

        }
    }
}
