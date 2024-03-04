using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Empleado
    {
        public int EmpleadoID { get; set; }
        public string Nombre { get; set; }
        public Negocio.Departamento Departamento { get; set; }
        public Negocio.Puesto Puesto { get; set; }
        public List<object> Empleados { get; set; }


        public static Dictionary<string, object> Add(Negocio.Empleado empleado)
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object>() { { "Resultado", false }, { "Excepcion", "" } };

            try
            {
                using (AccesoDatos.ARodriguezEstructuraEntities context = new AccesoDatos.ARodriguezEstructuraEntities())
                {
                    var filasAfectadas = context.EmpleadoAdd(empleado.Nombre, empleado.Puesto.PuestoID, empleado.Departamento.DepartamentoID);

                    if(filasAfectadas > 0)
                    {
                        diccionario["Resultado"] = true;
                    }
                    else
                    {
                        diccionario["Resultado"] = false;
                    }
                }
            }
            catch (Exception ex)
            {
                diccionario["Excepcion"] = ex.Message;
                diccionario["Resultado"] = false;
            }

            return diccionario;
        }

        public static Dictionary<string, object> GetByNombre(string nombre)
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object>() { {"Excepcion", ""}, {"Resultado", false}, {"Empleado", null}};

            try
            {
                using (AccesoDatos.ARodriguezEstructuraEntities context = new AccesoDatos.ARodriguezEstructuraEntities())
                {
                    var registros = context.EmpleadoGetByNombre(nombre).ToList();

                    if(registros != null)
                    {
                        Negocio.Empleado employee = new Empleado();
                        employee.Empleados = new List<object>();

                        foreach(var registro in registros)
                        {
                            Negocio.Empleado emp = new Empleado();
                            emp.EmpleadoID = registro.EmpleadoID;
                            emp.Nombre = registro.Nombre;
                            
                            employee.Empleados.Add(emp);
                        }

                        diccionario["Resultado"] = true;
                        diccionario["Empleado"] = employee;
                    }
                }
            }
            catch (Exception ex)
            {
                diccionario["Excepcion"] = ex.Message;
                diccionario["Resultado"] = false;
            }

            return diccionario;
        }

        public static Dictionary<string, object> GetAll()
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object>() { { "Excepcion", "" }, { "Resultado", false }, { "Empleado", null } };

            try
            {
                using (AccesoDatos.ARodriguezEstructuraEntities context = new AccesoDatos.ARodriguezEstructuraEntities())
                {
                    var registros = context.EmpleadoGetAll().ToList();

                    if (registros != null)
                    {
                        Negocio.Empleado employee = new Empleado();
                        employee.Empleados = new List<object>();

                        foreach (var registro in registros)
                        {
                            Negocio.Empleado emp = new Empleado();
                            emp.EmpleadoID = registro.EmpleadoId;
                            emp.Nombre = registro.Nombre;
                            emp.Puesto = new Puesto();
                            emp.Puesto.PuestoID = registro.PuestoId;
                            emp.Puesto.Descripcion = registro.DescripcionPuesto;
                            emp.Departamento = new Departamento();
                            emp.Departamento.DepartamentoID = registro.DepartamentoID;
                            emp.Departamento.Descripcion = registro.DescripcionDepartamento;

                            employee.Empleados.Add(emp);
                        }

                        diccionario["Resultado"] = true;
                        diccionario["Empleado"] = employee;
                    }
                }
            }
            catch (Exception ex)
            {
                diccionario["Excepcion"] = ex.Message;
                diccionario["Resultado"] = false;
            }

            return diccionario;
        }


        public static Dictionary<string, object> Delete(int EmpleadoID)
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object>() { { "Excepcion", "" }, { "Resultado", false } };

            try
            {
                using (SqlConnection context = new SqlConnection(AccesoDatos.ARodriguezEstructuraConnection.GetConnectionString()))
                {
                    string sentencia = "EmpleadoDelete";

                    SqlParameter[] parametros = new SqlParameter[1];
                    parametros[0] = new SqlParameter("@EmpleadoID", System.Data.SqlDbType.Int);
                    parametros[0].Value = EmpleadoID;

                    SqlCommand command = new SqlCommand(sentencia, context);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddRange(parametros);
                    command.Connection.Open();

                    var filasAfectadas = command.ExecuteNonQuery();

                    if(filasAfectadas > 0)
                    {
                        diccionario["Resultado"] = true;
                    }
                    else
                    {
                        diccionario["Resultado"] = false;
                    }
                }
            }
            catch (Exception ex)
            {
                diccionario["Excepcion"] = ex.Message;
                diccionario["Resultado"] = false;
            }

            return diccionario;
        }
    }
}
