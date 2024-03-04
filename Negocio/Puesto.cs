using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Puesto
    {
        public int PuestoID { get; set; }
        public string Descripcion { get; set; }
        public List<object> Puestos { get; set; }

        public static Dictionary<string, object> GetAll()
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object>() { { "Resultado", false }, { "Excepcion", "" }, { "Puesto", null } };

            try
            {
                using (AccesoDatos.ARodriguezEstructuraEntities context = new AccesoDatos.ARodriguezEstructuraEntities())
                {
                    var registros = context.PuestoGetAll().ToList();

                    if (registros != null)
                    {
                        Negocio.Puesto pu = new Puesto();
                        pu.Puestos = new List<object>();

                        foreach (var registro in registros)
                        {
                            Negocio.Puesto pue = new Puesto();
                            pue.PuestoID = registro.PuestoId;
                            pue.Descripcion = registro.Descripcion;

                            pu.Puestos.Add(pue);
                        }

                        diccionario["Resultado"] = true;
                        diccionario["Puesto"] = pu;
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
