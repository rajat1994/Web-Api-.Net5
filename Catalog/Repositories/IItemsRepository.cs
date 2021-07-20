
using System;
using System.Collections.Generic;
using Catalog.Entities;
using System.Linq;


namespace Catalog.Repositories
{

    public interface IItemsRepository
    {

        IEnumerable<Item> GetItems();
        Item GetItem(Guid id);

        void CreateItem(Item item);

    }

}