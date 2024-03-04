using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Departamento
    {
        public int DepartamentoID { get; set; }
        public string Descripcion { get; set; }
        public List<object> Departamentos { get; set; }

        public static Dictionary<string, object> GetAll()
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object>() { {"Resultado", false}, {"Excepcion", ""}, {"Departamento", null} };

            try
            {
                using (AccesoDatos.ARodriguezEstructuraEntities context = new AccesoDatos.ARodriguezEstructuraEntities())
                {
                    var registros = context.DepartamentoGetAll().ToList();

                    if(registros != null)
                    {
                        Negocio.Departamento dep = new Departamento();
                        dep.Departamentos = new List<object>();

                        foreach (var registro in registros)
                        {
                            Negocio.Departamento depa = new Departamento();
                            depa.DepartamentoID = registro.DepartamentoId;
                            depa.Descripcion = registro.Descripcion;

                            dep.Departamentos.Add(depa);
                        }

                        diccionario["Resultado"] = true;
                        diccionario["Departamento"] = dep;
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
