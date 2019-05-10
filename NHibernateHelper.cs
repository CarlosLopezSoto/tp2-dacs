using System;
using System.Collections.Generic;
using System.Text;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System.Data.SqlClient;
using NHibernate.Driver;

namespace TP2
{
    class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;
        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                    InitializeSessionFactory();


                    return _sessionFactory;
                
            }
        }
        private static void InitializeSessionFactory()
        {
            _sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql7
                .ConnectionString(@"Server=(localdb)\mssqllocaldb;Database=TP2;Trusted_Connection=True;")
                .ShowSql())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Car>())
                .ExposeConfiguration(cfg => new SchemaExport(cfg)
                .Create(true, false))
                .BuildSessionFactory();
        }
        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}
