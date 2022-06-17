using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DALException
{
    public class TemporaryExceptionDAL : Exception
    {
        public TemporaryExceptionDAL()
        {
        }

        public TemporaryExceptionDAL(string? message) : base(message)
        {
        }

        public TemporaryExceptionDAL(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected TemporaryExceptionDAL(SerializationInfo info, StreamingContext context) : base(info, context)
        {

            /*            try
                        {

                        }
                        catch (InvalidOperationException ex)
                        {
                            databaseConnection.Close();
                            throw new TemporaryExceptionDAL("Temporary error with connection");
                        }
                        catch (IOException ex)
                        {
                            databaseConnection.Close();
                            throw new TemporaryExceptionDAL("Temporary error with connection");
                        }
                        catch (SqlException ex)
                        {
                            databaseConnection.Close();
                            throw new TemporaryExceptionDAL("No connection with server");
                        }
                        catch (Exception ex)
                        {
                            databaseConnection.Close();
                            throw new PermanentExceptionDAL("errpr PLS check twitter for updates");
                        }


                        catch (TemporaryExceptionDAL ex)
                        {
                            ViewBag.Error = ex.Message + " PLS try again later";
                            return Redirect("/");
                        }
                        catch (PermanentExceptionDAL ex)
                        {
                            return Content(ex.Message);
                        }
            */
        }



    }
}
