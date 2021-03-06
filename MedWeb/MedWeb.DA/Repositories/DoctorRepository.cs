﻿using MedWeb.DA.Interfaces;
using MedWeb.DA.Tables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MedWeb.DA.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private ApplicationDbContext _dbContext;

        public DoctorRepository()
        {
            _dbContext = ApplicationDbContext.Create();
        }

        public List<Doctor> GetAllDoctors()
        {
            return _dbContext
                .Doctor
                .ToList();
        }

        public Doctor GetDoctorById(int id)
        {
            return _dbContext
                .Doctor
                .Where(x => x.Id.Equals(id))
                .FirstOrDefault();
        }

        public Doctor GetDoctorByLastName(string lastName)
        {
            return _dbContext
                .Doctor
                .Where(x => x.LastName.Equals(lastName))
                .FirstOrDefault();
        }

        public List<Doctor> GetDoctorsBySpecialization(string specName)
        {
            return _dbContext
                .Doctor
                .Where(x => x.Specialization.Name.Equals(specName))
                .ToList();
        }

        public Doctor DetailsOfDoctor(int doctorId)
        {
            return _dbContext
                 .Doctor
                 .Select(x => x)
                 .Where(x => x.Id == doctorId)
                 .FirstOrDefault();
        }

        public void AddDoctor(Doctor doctor)
        {
            _dbContext.Doctor.Add(doctor);
            _dbContext.SaveChanges();
        }

        public bool UpdateDoctor(int doctorId, Doctor doctor)
        {
            try
            {
                var DoctorModel = _dbContext
                    .Doctor
                    .Select(x => x)
                    .Where(x => x.Id == doctorId)
                    .FirstOrDefault();

                DoctorModel.FirstName = doctor.FirstName;
                DoctorModel.LastName = doctor.LastName;
                DoctorModel.Specialization.Name = doctor.Specialization.Name;
                DoctorModel.Specialization.InsertTime = DateTime.Now;
                _dbContext.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteDoctor(int doctorId)
        {
            try
            {
                _dbContext.Doctor.Remove(_dbContext.Doctor.Find(doctorId));
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