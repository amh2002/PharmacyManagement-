﻿using Hope.EntityComponent.DBEntities;
using Hope.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Repository.Repository
{
    public class ModuleRoleRepository : Repository<ModuleRole>, IRepository.IModuleRoleRepository
    {

        public ModuleRoleRepository() 
        {

        }

    }
}
