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
    public class UserRepository : UserRepositoryBase, IUserRepository
    {
        public UserRepository(Camm2Context context)
            :base(context)
        {
        }
	}
}
