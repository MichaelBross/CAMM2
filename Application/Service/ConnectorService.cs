using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using Domain;
using Application;
using Application.Interfaces;

namespace Application.Service
{
    public class ConnectorService : ConnectorServiceBase, IConnectorService
    {
	    private readonly IUnitOfWork _unitOfWork;

        public ConnectorService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
	}
}
