﻿using System;
using System.Collections.Generic;

namespace set_basic_aspnet_mvc.Domain.Entities
{
    public class PagedList<TEntity> where TEntity : BaseEntity
    {
        public int Index { get; set; }
        public int Size { get; set; }
        public long TotalCount { get; set; }
        public int TotalPageCount { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
        public List<TEntity> Items { get; set; }

        public PagedList(int pageIndex, int pageSize, long totalCount, IEnumerable<TEntity> source)
        {
            Items = new List<TEntity>();
            Items.AddRange(source);

            Index = pageIndex;
            Size = pageSize;
            TotalCount = totalCount;
            TotalPageCount = (int)Math.Ceiling(totalCount / (double)pageSize);
            HasPreviousPage = Index > 1;
            HasNextPage = Index < TotalPageCount;
        }
    }
}