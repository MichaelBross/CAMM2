using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using Persistance.Repository;
using Domain;
using Application;
using Application.Interfaces;

namespace Persistance
{
    public class ConnectorRepository : ConnectorRepositoryBase, IConnectorRepository
    {
        public ConnectorRepository(Camm2Context context)
            :base(context)
        {
        }

        public Connector GetWithDocuments(int connectorId)
        {
            return _entity.Include("Documents").Where(c => c.Id == connectorId).FirstOrDefault();
        }
	}
}
