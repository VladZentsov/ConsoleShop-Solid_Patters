using BLL.Dto;
using ClassLibrary1.Entities;
using ClassLibrary1.Validation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BLL.Validation
{
    public static class RepoValidationHelper
    {
        public static void CheckCreationModelId(BaseDto checkEntity, IEnumerable<BaseEntity> listEntities)
        {
            if(checkEntity.Id==0)
            {
                listEntities=listEntities.OrderBy(e=>e.Id);
                bool flag=true;
                int counterId = 1;
                foreach (var entity in listEntities)
                {
                    if (counterId == entity.Id)
                        counterId++;
                    else
                    {
                        checkEntity.Id = counterId;
                        flag=false;
                        break;
                    }
                }
                if(flag)
                    checkEntity.Id = counterId;
            }
            else
            {
                foreach (var entity in listEntities)
                {
                    if (entity.Id == checkEntity.Id)
                        throw new ShopException("Entity id not unique");
                }
            }
           
        }
    }
}
