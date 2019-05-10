using FluentNHibernate.Mapping;
using System;

namespace TP2
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    // ALTA
                    //Console.WriteLine("Esciba Nombre marca");                                                              
                    //var fordMake = new Make
                    //{
                    //    Name = Console.ReadLine() 
                    //};
                    //Console.WriteLine("Esciba Nombre Modelo");
                    //var fiestaModel = new Model
                    //{
                    //    Name = Console.ReadLine(),
                    //    Make = fordMake
                    //};
                    //var car = new Car
                    //{
                    //    Make = fordMake,
                    //    Model = fiestaModel,
                    //    Title = "Carlos Car"
                    //};
                    //session.Save(car);
                    //transaction.Commit();
                    //Console.WriteLine("Create Car" + car.Title);
                   
                    //BUSQUEDA
                    Console.WriteLine("Escriba titulo de auto a buscar");
                    string tit = Console.ReadLine();
                    Car Cars = null;
                    var makefound = session.QueryOver<Car>()
                        .Where(p => p.Title == tit).List();
                    if (makefound == null)
                    {
                        Console.WriteLine("Ningun Auto se llama con ese titulo");
                    }
                    else
                    {
                        Cars = makefound[0];
                        Console.WriteLine(Cars.Id);
                        Console.WriteLine(Cars.Title);
                    }
                    // BAJA

                    //session.Delete(Cars);
                    //transaction.Commit();
                    //Console.WriteLine("Se Borro");

                    // MODIFICACION
                    Console.WriteLine("Escriba Titulo de Auto a Cambiar");
                    Cars.Title = Console.ReadLine();
                    transaction.Commit();
                    Console.WriteLine("Se cambio el titulo");



                    Console.ReadLine();
                }
            }
        }
    }
    public class MakeMap : ClassMap<Make>
    {
        public MakeMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
        }
    }

    public class ModelMap : ClassMap<Model>
    {
        public ModelMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            References(x => x.Make).Cascade.All();
        }
    }

    public class CarMap : ClassMap<Car>
    {
        public CarMap()
        {
            Id(x => x.Id);
            Map(x => x.Title);
            References(x => x.Make).Cascade.All();
            References(x => x.Model).Cascade.All();
        }
    }
    public class Make
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }

    }
    public class Model
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual Make Make { get; set; }

    }
    public class Car
    {
        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual Make Make { get; set; }
        public virtual Model Model { get; set; }
    }

}
