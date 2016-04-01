using System.Collections.Generic;
using Kurogane.Data.RestApi.Infrastructure;
using Kurogane.Data.RestApi.Models;

namespace Kurogane.Data.RestApi.Services
{
    public interface ISmashAttributeService
    {
        IEnumerable<SmashAttributeType> GetAttributes();
        SmashAttributeType GetAttribute(int id);
        void CreateSmashAttribute(SmashAttributeType attribute);
        void UpdateSmashAttribute(SmashAttributeType attribute);
        void SaveAttribute();
        void DeleteAttribute(SmashAttributeType attribute);
    }

    public class SmashAttributeService : ISmashAttributeService
    {
        private readonly ISmashAttributeRepository _smashAttributeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SmashAttributeService(ISmashAttributeRepository smashAttributeRepository, IUnitOfWork unitOfWork)
        {
            _smashAttributeRepository = smashAttributeRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<SmashAttributeType> GetAttributes()
        {
            var attributes = _smashAttributeRepository.GetAll();
            return attributes;
        }

        public SmashAttributeType GetAttribute(int id)
        {
            var attribute = _smashAttributeRepository.GetById(id);
            return attribute;
        }

        public void CreateSmashAttribute(SmashAttributeType attribute)
        {
            _smashAttributeRepository.Add(attribute);
            SaveAttribute();
        }

        public void UpdateSmashAttribute(SmashAttributeType attribute)
        {
            _smashAttributeRepository.Update(attribute);
            SaveAttribute();
        }

        public void SaveAttribute()
        {
            _unitOfWork.Commit();
        }

        public void DeleteAttribute(SmashAttributeType attribute)
        {
            _smashAttributeRepository.Delete(attribute);
            SaveAttribute();
        }
    }
}