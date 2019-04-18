using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;

namespace Application.Items.Queries.GetItemList
{
    public class GetItemList : IGetItemList
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetItemList(IUnitOfWork repo)
        {
            _unitOfWork = repo;
        }
        public List<ItemVM> Execute()
        {
            return _unitOfWork.Items.GetAll()
                .Select(p => new ItemVM()
                {
                    Id = p.Id,
                    Code = p.Code,
                    Description = p.Description,
                    QtyOnHand = p.QtyOnHand,
                    UnitsOfMeasure = p.UnitsOfMeasure
                }).ToList();

        }
    }
}
