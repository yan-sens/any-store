﻿using DAL.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetCategories();
        Task<List<Category>> GetCategoriesWithChild();
        Task AddCategory(Category category);
        Task UpdateCategory(Category category);
        Task RemoveCategory(Guid id);
    }
}
