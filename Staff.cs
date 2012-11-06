using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDT_A1_s3252820
{
    class Staff : Person
    {
         public Staff(string name, string id, string email) :
            base(name, id, email){}

         public void checkRoomAvail()
         {
         }

         public bool createBookingSlot()
         {
             return false;
         }

         public void deleteBookingSlot()
         {

         }

         public void listBookingSchedule()
         {

         }
    }
}
