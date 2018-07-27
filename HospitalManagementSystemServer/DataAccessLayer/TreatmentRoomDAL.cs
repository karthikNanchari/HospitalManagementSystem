using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;


namespace DataAccessLayer
{
    /// <summary>
    /// Treatment room data accesslayer, using Linq Create, insert, update and delete operations are performed
    /// </summary>
    public class TreatmentRoomDAL
    {
        //Method to Insertnew Treatment room record, with treatment business object as return type and treatment business object as parameter
        public TreatmentRoomBO InsertTreatmentRoom(TreatmentRoomBO treatmentRoomBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    Treatment_Room trmntRoom = new Treatment_Room();

                    objHmsDataContext.Treatment_Rooms.InsertOnSubmit(ConvertBOToTreatmentRoom(trmntRoom, treatmentRoomBO));

                    Treatment_Room newtrmntRoom = objHmsDataContext.Treatment_Rooms.SingleOrDefault(trmntroom => (trmntroom.Room_ID == treatmentRoomBO.room_ID && trmntroom.DateOfJoin == treatmentRoomBO.date));

                    return ConvertTrmtRmToBO(newtrmntRoom);
                }
            }
            catch(Exception e)
            {
                TreatmentRoomBO treatmentRoom_BO = new TreatmentRoomBO();
                return treatmentRoom_BO;
            }
        }

        public TreatmentRoomBO CheckAvailabilityTreatmentRoom(TreatmentRoomBO treatmentRoomBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    Treatment_Room trmntRoom = new Treatment_Room();

                    // objHmsDataContext.Treatment_Rooms.InsertOnSubmit(ConvertBOToTreatmentRoom(trmntRoom, treatmentRoomBO));

                    //if (objHmsDataContext.Treatment_Rooms.Where(trmntroom => (trmntroom.Room_ID == treatmentRoomBO.room_ID && trmntroom.DateOfJoin == treatmentRoomBO.date && trmntroom.IsBooked == true)).FirstOrDefault())
                    //{

                    //}
                    TreatmentRoomBO newtrmntRoom = objHmsDataContext.Treatment_Rooms.Where(trmntroom => (trmntroom.Room_ID == treatmentRoomBO.room_ID && trmntroom.DateOfJoin == treatmentRoomBO.date 
                    && trmntroom.IsBooked == true)).Select( t => new TreatmentRoomBO {
                        timings = Convert.ToString(t.Timings)
                    }).First();

                    return newtrmntRoom;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        //Method to convert BO to TreatmentRoom
        public Treatment_Room ConvertBOToTreatmentRoom(Treatment_Room treatmentRoom, TreatmentRoomBO treatmentRoomBO)
        {
            if (treatmentRoomBO.room_ID != null)
            {
                treatmentRoom.Room_ID = treatmentRoomBO.room_ID;
            }

            if (!string.IsNullOrEmpty(treatmentRoomBO.timings))
            {
                treatmentRoom.Timings = TimeSpan.Parse(treatmentRoomBO.timings);
            }

            if (treatmentRoomBO.date != DateTime.MinValue)
            {
                treatmentRoom.DateOfJoin = treatmentRoomBO.date;
            }
            if(treatmentRoomBO.patient_ID != 0 )
            {
                treatmentRoom.Patient_ID = treatmentRoomBO.patient_ID;
                
            }
            if(treatmentRoomBO.appointment_ID != 0)
            {
                treatmentRoom.Appointment_ID = treatmentRoomBO.appointment_ID;
            }
            treatmentRoom.IsBooked = treatmentRoomBO.isBooked;

            return treatmentRoom;

        }

        //Method to convert TreatmentRoom to BO
        public TreatmentRoomBO ConvertTrmtRmToBO(Treatment_Room treatmentRoom)
        {
            TreatmentRoomBO treatmentRoomBO = new TreatmentRoomBO(treatmentRoom.Room_ID, Convert.ToString(treatmentRoom.Timings),
                                                                    treatmentRoom.DateOfJoin, treatmentRoom.IsBooked, treatmentRoom.Patient_ID);

            return treatmentRoomBO;
        }

        //Method to get TreatmentRoom details, with treatment business object as return type and treatment business object as parameter
        public TreatmentRoomBO GetTreatmentRoom(TreatmentRoomBO treatmentRoomBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();
                    Treatment_Room treatmentRoom = objHmsDataContext.Treatment_Rooms.SingleOrDefault(trmntRoom => (trmntRoom.Room_ID == treatmentRoomBO.room_ID &&
                                                                                            trmntRoom.DateOfJoin == treatmentRoomBO.date));
                    return (ConvertTrmtRmToBO(treatmentRoom));
                }
            }
            catch(Exception e)
            {
                TreatmentRoomBO trmntRoomBO = new TreatmentRoomBO();
                return trmntRoomBO;
            }
        }

        //Method to update TreatmentRoom details, with string as return type and treatment business object as parameter
        public IEnumerable<TreatmentRoomBO> UpdateTreatmentRoom(TreatmentRoomBO treatmentRoomBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    Treatment_Room treatmentRoom = objHmsDataContext.Treatment_Rooms.Where(trmntRooms => (trmntRooms.Room_ID == treatmentRoomBO.room_ID && trmntRooms.Appointment_ID == treatmentRoomBO.appointment_ID)).FirstOrDefault();

                    if (treatmentRoom != null)
                    {
                        Treatment_Room updateTrmntRoom = ConvertBOToTreatmentRoom(treatmentRoom, treatmentRoomBO);
                    }
                    objHmsDataContext.SubmitChanges();


                    TreatmentRoom_Record tr = new TreatmentRoom_Record();
                    tr.Appointment_ID = treatmentRoomBO.appointment_ID;
                    tr.DateOfJoin = treatmentRoomBO.date;
                    tr.Patient_ID = treatmentRoomBO.patient_ID;
                    tr.Room_ID = treatmentRoomBO.room_ID;
                    //tr.Timings = treatmentRoomBO.timings;
                    if (treatmentRoomBO.isBooked != false)
                    {
                        tr.IsBooked = true;
                    }
                    else
                    {
                        tr.IsBooked = false;
                    }
                    //objHmsDataContext.TreatmentRoom_Records.InsertOnSubmit(tr);
                    objHmsDataContext.SubmitChanges();
                    UpdateTreatmentRoomRecord(tr);
                    return GetAllTreatmentRooms();
                }

            }
            catch(Exception e)
            {
                IEnumerable<TreatmentRoomBO> room = null;
                return room;
            }
        }

        //Method to Delete TreatmentRoom record, wtih string as return type and treatment business object as parameter
        public string DeleteTreatmentRoom(TreatmentRoomBO treatmentRoomBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    Treatment_Room treatmentRoom = new Treatment_Room();

                    objHmsDataContext.Treatment_Rooms.DeleteOnSubmit(ConvertBOToTreatmentRoom(treatmentRoom, treatmentRoomBO));

                    objHmsDataContext.SubmitChanges();

                    return "TreatmentRoom details deleted Successfully";
                }
            }
            catch(Exception e)
            {
                return "Unable to Delete treatmentRoom details please try again later";
            }
        }


        public IEnumerable<TreatmentRoomBO> GetAllTreatmentRooms()
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    IEnumerable<TreatmentRoomBO> treatmentRoom = objHmsDataContext.Treatment_Rooms.Join(objHmsDataContext.Patients,
                        t => t.Patient_ID,
                        p => p.Patient_ID,
                        (t,p) => new TreatmentRoomBO { 
                        patient_ID = t.Patient_ID,
                        timings =Convert.ToString(t.Timings),
                        isBooked = t.IsBooked,
                        date = t.DateOfJoin,
                        room_ID = t.Room_ID,
                        PatientFirstName = p.First_Name,
                        appointment_ID = t.Appointment_ID
                    }).ToArray();
                    return treatmentRoom;
                }
            }
            catch (Exception e)
            {
                IEnumerable<TreatmentRoomBO> trmntRoomBO = null;
                return trmntRoomBO;
            }
        }

        public IEnumerable<TreatmentRoomBO> GetAvailableRooms(TreatmentRoomBO room)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    //List<TreatmentRoomBO> treatmentRoom = objHmsDataContext.TreatmentRoom_Records
                    //    .Where(t => (t.DateOfJoin != room.date && t.IsBooked == false))
                    //    .Select(t => new TreatmentRoomBO
                    //{
                    //    patient_ID = t.Patient_ID,
                    //    timings    = Convert.ToString(t.Timings),
                    //    isBooked   = t.IsBooked,
                    //    date       = t.DateOfJoin,
                    //    room_ID    = t.Room_ID
                    //}).ToList();


                    List<TreatmentRoomBO> treatmentRoomRec = objHmsDataContext.ExecuteQuery<TreatmentRoomBO>("SELECT Room_ID, Appointment_ID, DateOfJoin,IsBooked,Patient_ID FROM TreatmentRoom_Records WHERE Room_ID IN( SELECT MAX (Room_ID) FROM TreatmentRoom_Records GROUP BY Room_ID)").ToList();
                    List<TreatmentRoomBO> treatmentRoomRecRemove = new List<TreatmentRoomBO>();
                    List<TreatmentRoomBO> treatmentRoomRecRemoveNew = new List<TreatmentRoomBO>();
                    foreach (var r in treatmentRoomRec)
                    {
                        if (r.DateOfJoin == room.date) { treatmentRoomRecRemove.Add(r); }
                    }
                    foreach (var r in treatmentRoomRecRemove)
                    {
                        if (r.isBooked == true) { treatmentRoomRecRemoveNew.Add(r); }
                    }
                    foreach (var r  in treatmentRoomRecRemoveNew) {
                        treatmentRoomRec = treatmentRoomRec.Where(rr => rr.room_ID != r.room_ID).ToList();

                        //treatmentRoomRec = treatmentRoomRec.;
                    }

                    return treatmentRoomRec;
                }
            }
            catch (Exception e)
            {
                IEnumerable<TreatmentRoomBO> trmntRoomBO = null;
                return trmntRoomBO;
            }
        }


        public IEnumerable<TreatmentRoomBO> GetBookedRooms(PatientBO pat)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();
                    IEnumerable<TreatmentRoomBO> treatmentRoom;
                     treatmentRoom = objHmsDataContext.TreatmentRoom_Records
                        .Where(t => (t.IsBooked == true) && (t.Patient_ID == pat.pid))
                        .Select(t => new TreatmentRoomBO
                        {
                            patient_ID = t.Patient_ID,
                            timings = Convert.ToString(t.Timings),
                            isBooked = t.IsBooked,
                            date = t.DateOfJoin,
                            room_ID = t.Room_ID,
                            appointment_ID = t.Appointment_ID
                        }).ToArray();
                    return treatmentRoom;
                }
            }
            catch (Exception e)
            {
                IEnumerable<TreatmentRoomBO> trmntRoomBO = null;
                return trmntRoomBO;
            }
        }

        public IEnumerable<TreatmentRoomBO> GetRoomsRecordsByPatID(PatientBO pat)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();
                    IEnumerable<TreatmentRoomBO> treatmentRoom;
                    treatmentRoom = objHmsDataContext.TreatmentRoom_Records
                        .Join(objHmsDataContext.Patients,
                        t => t.Patient_ID,
                        p => p.Patient_ID,
                        (t, p) => new TreatmentRoomBO
                        {
                            patient_ID = t.Patient_ID,
                            timings = Convert.ToString(t.Timings),
                            isBooked = t.IsBooked,
                            date = t.DateOfJoin,
                            room_ID = t.Room_ID,
                            appointment_ID = t.Appointment_ID,
                            PatientFirstName = p.First_Name
                       }).Where(t => (t.patient_ID == pat.pid)).ToArray();
                    return treatmentRoom;
                }
            }
            catch (Exception e)
            {
                IEnumerable<TreatmentRoomBO> trmntRoomBO = null;
                return trmntRoomBO;
            }
        }

        public IEnumerable<TreatmentRoomBO> UpdateTreatmentRoomRecord(TreatmentRoom_Record treatmentRoomRec)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    var treatmentRmRec = objHmsDataContext.TreatmentRoom_Records.Where(trmntRooms => (trmntRooms.Room_ID == treatmentRoomRec.Room_ID && trmntRooms.Appointment_ID == treatmentRoomRec.Appointment_ID)).FirstOrDefault();

                    treatmentRmRec.IsBooked = treatmentRoomRec.IsBooked;
                    objHmsDataContext.SubmitChanges();

                    return GetAllTreatmentRooms();
                }

            }
            catch (Exception e)
            {
                IEnumerable<TreatmentRoomBO> room = null;
                return room;
            }
        }

        public TreatmentRoomBO InsertTreatmentRoomRec(TreatmentRoomBO treatmentRoomBO)
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    TreatmentRoom_Record trmntRoom = new TreatmentRoom_Record();

                    objHmsDataContext.TreatmentRoom_Records.InsertOnSubmit(ConvertBOToTreatmentRoomRec(trmntRoom, treatmentRoomBO));
                    objHmsDataContext.SubmitChanges();

                    TreatmentRoom_Record newtrmntRoom = objHmsDataContext.TreatmentRoom_Records.SingleOrDefault(trmntroom => (trmntroom.Room_ID == treatmentRoomBO.room_ID && trmntroom.DateOfJoin == treatmentRoomBO.date));

                    return ConvertTrmtRmRecToBO(newtrmntRoom);
                }
            }
            catch (Exception e)
            {
                TreatmentRoomBO treatmentRoom_BO = new TreatmentRoomBO();
                return treatmentRoom_BO;
            }
        }

        public IEnumerable<TreatmentRoomBO> GetAllTreatmentRoomRecs()
        {
            try
            {
                using (HospitalManagementSystemDataContext objHmsDataContext = new HospitalManagementSystemDataContext(Utils.ConnectionString))
                {
                    if (objHmsDataContext.Connection.State == System.Data.ConnectionState.Closed)
                        objHmsDataContext.Connection.Open();

                    //IEnumerable<TreatmentRoomBO> treatmentRoom = objHmsDataContext.TreatmentRoom_Records.Join(objHmsDataContext.Patients,
                    //    t => t.Patient_ID,
                    //    p => p.Patient_ID,
                    //    (t, p) => new TreatmentRoomBO
                    //    {
                    //        patient_ID = t.Patient_ID,
                    //        timings = Convert.ToString(t.Timings),
                    //        isBooked = t.IsBooked,
                    //        date = t.DateOfJoin,
                    //        room_ID = t.Room_ID,
                    //        PatientFirstName = p.First_Name,
                    //        appointment_ID = t.Appointment_ID
                    //    }).ToArray();

                    List<TreatmentRoomBO> treatmentRoomRec = objHmsDataContext.ExecuteQuery<TreatmentRoomBO>("SELECT Room_ID, Appointment_ID, DateOfJoin,IsBooked,Patient_ID  FROM TreatmentRoom_Records WHERE Room_ID IN( SELECT MAX (Room_ID) FROM TreatmentRoom_Records GROUP BY Room_ID)").ToList();

                    foreach (var t in treatmentRoomRec)
                    {
                        t.PatientFirstName = objHmsDataContext.Patients.Where(p => p.Patient_ID == t.patient_ID).Select(p => p.First_Name).First();
                        t.timings = "09:00";
                        t.date = t.DateOfJoin;
                    }

                    return treatmentRoomRec;
                }
            }
            catch (Exception e)
            {
                IEnumerable<TreatmentRoomBO> trmntRoomBO = null;
                return trmntRoomBO;
            }
        }


        public TreatmentRoom_Record ConvertBOToTreatmentRoomRec(TreatmentRoom_Record treatmentRoom, TreatmentRoomBO treatmentRoomBO)
        {
            if (treatmentRoomBO.room_ID != null)
            {
                treatmentRoom.Room_ID = treatmentRoomBO.room_ID;
            }

            if (!string.IsNullOrEmpty(treatmentRoomBO.timings))
            {
                treatmentRoom.Timings = TimeSpan.Parse(treatmentRoomBO.timings);
            }

            if (treatmentRoomBO.date != DateTime.MinValue)
            {
                treatmentRoom.DateOfJoin = treatmentRoomBO.date;
            }
            if (treatmentRoomBO.patient_ID != 0)
            {
                treatmentRoom.Patient_ID = treatmentRoomBO.patient_ID;

            }
            if (treatmentRoomBO.appointment_ID != 0)
            {
                treatmentRoom.Appointment_ID = treatmentRoomBO.appointment_ID;
            }
            treatmentRoom.IsBooked = treatmentRoomBO.isBooked;

            return treatmentRoom;

        }
        public TreatmentRoomBO ConvertTrmtRmRecToBO(TreatmentRoom_Record treatmentRoom)
        {
            TreatmentRoomBO treatmentRoomBO = new TreatmentRoomBO(treatmentRoom.Room_ID, Convert.ToString(treatmentRoom.Timings),
                                                                    treatmentRoom.DateOfJoin, treatmentRoom.IsBooked, treatmentRoom.Patient_ID);

            return treatmentRoomBO;
        }
    }
}
