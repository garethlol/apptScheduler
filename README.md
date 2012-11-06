apptScheduler
=============

Simple menu-based console application in C# for Appointment Scheduling and Reserving

Overview
=============

Appointment Scheduler performs both scheduling of appointments and reserving slots for
students scheduling meetings with staff Members in certain rooms.

The scheduler performs basic functions such as.

  a) 2 types of users for the system, Staff and Students
  b) Staff are able to check room availability, create/delete slots and list their schedule
  c) Students are able to view the free slots for each staff member and make/delete a booking

System preloads data from a txt file (In this case it is called users.txt with both staff and
student details.

Data Validation
=============

  a) Staff ID always starts with e, then 7 numbers
  b) Student ID always starts with s, then 7 numbers
  c) Email for a staff always ends with emu.rmit.edu.au
  d) Email for a student always ends with student.rmit.edu.au
  e) Each ID is unique (Staff and Student alike)

The system must throw an error if the text file has invalid data.

Business Rules
=============

  a) 1 hour slots
  b) 1 staff member : Maximum 4 slots a day
  c) Must be booked in 9am - 2pm inclusive
  d) Each room : Maximum 2 slots a day
  e) Staff cannot delete a slot if booked by a student
  f) 1 Student : 1 Booking a day
  g) 1 Slot : 1 Student