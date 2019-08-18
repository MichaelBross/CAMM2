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
    public class AssemblyComponentService : AssemblyComponentServiceBase, IAssemblyComponentService
    {
	    private readonly IUnitOfWork _unitOfWork;

        public AssemblyComponentService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
	}
}
