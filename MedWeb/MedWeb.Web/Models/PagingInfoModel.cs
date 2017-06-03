﻿using System;
using System.Collections.Generic;

namespace MedWeb.Web.Models
{
    public class PagingInfoModel
    {
        public List<string> Items { get; set; }
        public PageInfo PageInfo{ get; set; }
    }

    public class PageInfo
    {
        public int NumOfItems { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int NumOfPages { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }

        public PageInfo(int numOfItems, int? page, int pageSize = 5)
        {
            var numOfPages = (int)Math.Ceiling((decimal)numOfItems / (decimal)pageSize);
            var currentPage = page != null ? (int)page : 1;
            var startPage = 1;
            var endPage = numOfPages;
            if (startPage <= 0)
            {
                endPage -= (startPage - 1);
                startPage = 1;
            }
            if (endPage > numOfPages)
            {
                endPage = numOfPages;
                if (endPage > pageSize)
                    startPage = endPage - (pageSize - 1);
            }

            NumOfItems = numOfItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            NumOfPages = numOfPages;
            StartPage = startPage;
            EndPage = EndPage;
        }
    }
}