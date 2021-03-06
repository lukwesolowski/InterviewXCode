﻿using MedWeb.DA.Interfaces;
using MedWeb.DA.Tables;
using System.Collections.Generic;
using System.Linq;

namespace MedWeb.DA.Repositories
{
    public class SpecializationRepository : ISpecializationRepository
    {
        private ApplicationDbContext _dbContext;

        public SpecializationRepository()
        {
            _dbContext = ApplicationDbContext.Create();
        }

        public List<Specialization> GetAllSpecializations()
        {
            return _dbContext
                .Specialization
                .ToList();
        }

        public Specialization GetSpeacializationByName(string specName)
        {
            return _dbContext
                .Specialization
                .Where(x => x.Name.Equals(specName))
                .FirstOrDefault();
        }

        public void AddSpecializaion(Specialization specialization)
        {
            _dbContext.Specialization.Add(specialization);
            _dbContext.SaveChanges();
        }

        public bool ChangeSpecializationName(Specialization SpecializationModel)
        {
            try
            {
                var SpecModel = _dbContext
                    .Specialization
                    .Select(x => x)
                    .Where(x => x.Id == SpecializationModel.Id)
                    .FirstOrDefault();

                SpecModel.Name = SpecializationModel.Name;
                _dbContext.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteSpecialization(int specId)
        {
            try
            {
                _dbContext.Specialization.Remove(_dbContext.Specialization.Find(specId));
                _dbContext.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}