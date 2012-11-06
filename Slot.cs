using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDT_A1_s3252820
{
    class Slot
    {
        private string room;
        private string startTime;
        private string endTime;
        private string staffID;
        private string studentID = "-";

        public Slot(string room, string startTime, string endTime, string staffID, string studentID)
        {
            this.room = room;
            this.startTime = startTime;
            this.endTime = endTime;
            this.staffID = staffID;
            this.studentID = studentID;
        }

        public Slot(string room, string startTime, string endTime, string staffID)
        {
            this.room = room;
            this.startTime = startTime;
            this.endTime = endTime;
            this.staffID = staffID;
            this.studentID = "-";
        }

        public string Room
        {
            get
            {
                return room;
            }
            set
            {
                room = value;
            }
        }

        public string StartTime
        {
            get
            {
                return startTime;
            }
            set
            {
                startTime = value;
            }
        }

        public string EndTime
        {
            get
            {
                return endTime;
            }
            set
            {
                endTime = value;
            }
        }

        public string StaffID
        {
            get
            {
                return staffID;
            }
            set
            {
                staffID = value;
            }
        }

        public string StudentID
        {
            get
            {
                return studentID;
            }
            set
            {
                studentID = value;
            }
        }
    }
}
