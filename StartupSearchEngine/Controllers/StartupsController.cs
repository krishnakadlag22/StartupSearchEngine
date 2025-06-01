using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StartupSearchEngine.Models;
using X.PagedList;
using X.PagedList.Extensions;

public class StartupsController : Controller
{
    private readonly StartupDbContext _context;

    public StartupsController(StartupDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(string searchTerm, string searchBy, int? page)
    {
        // Get all data first
        var startups = await _context.CleanedStartups.ToListAsync();

        // If searchTerm is provided
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            searchTerm = searchTerm.ToLower();

            // Filter based on specific field (searchBy)
            startups = startups.Where(s =>
                (searchBy == "Company" && s.Company?.ToLower().Contains(searchTerm) == true) ||
                (searchBy == "Founders" && s.Founders?.ToLower().Contains(searchTerm) == true) ||
                (searchBy == "InvestmentStage" && s.InvestmentStage?.ToLower().Contains(searchTerm) == true) ||
                (searchBy == "StartingYear" && (s.StartingYear?.ToLower().Contains(searchTerm) == true)) ||
                (searchBy=="Industry" && (s.Industry?.ToLower().Contains(searchTerm)==true))||
                string.IsNullOrEmpty(searchBy) && (
                    (s.Company?.ToLower().Contains(searchTerm) == true) ||
                    (s.Founders?.ToLower().Contains(searchTerm) == true) ||
                    (s.InvestmentStage?.ToLower().Contains(searchTerm) == true) ||
                    (s.StartingYear?.ToLower().Contains(searchTerm) == true) ||
                    (s.City?.ToLower().Contains(searchTerm) == true) ||
                    (s.Description?.ToLower().Contains(searchTerm) == true) ||
                    (s.EmployeeRange?.ToLower().Contains(searchTerm) == true) ||
                    (s.Industry?.ToLower().Contains(searchTerm) == true) ||
                    (s.NoOfInvestors?.ToLower().Contains(searchTerm) == true)
                )
            ).ToList();
        }

        // Custom merge sort logic based on selected column
        if (!string.IsNullOrEmpty(searchBy))
        {
            startups = SortStartups(startups, searchBy);
        }

        // Paging
        int pageSize = 5;
        int pageNumber = page ?? 1;

        return View(startups.ToPagedList(pageNumber, pageSize));
    }


    // Sorting dispatcher
    private List<CleanedStartup> SortStartups(List<CleanedStartup> data, string column)
    {
        return column.ToLower() switch
        {
            "company" => MergeSortByCompany(data),
            "founders" => MergeSortByFounders(data),
            "investmentstage" => MergeSortByInvestmentStage(data),
            "startingyear" => MergeSortByStartingYear(data),
            _ => data
        };
    }

    // Merge Sort by Company
    private List<CleanedStartup> MergeSortByCompany(List<CleanedStartup> list)
    {
        if (list.Count <= 1) return list;
        int mid = list.Count / 2;
        var left = MergeSortByCompany(list.GetRange(0, mid));
        var right = MergeSortByCompany(list.GetRange(mid, list.Count - mid));
        return MergeByCompany(left, right);
    }
    private List<CleanedStartup> MergeByCompany(List<CleanedStartup> left, List<CleanedStartup> right)
    {
        var result = new List<CleanedStartup>();
        int i = 0, j = 0;
        while (i < left.Count && j < right.Count)
        {
            if (string.Compare(left[i].Company ?? "", right[j].Company ?? "", true) <= 0)
                result.Add(left[i++]);
            else
                result.Add(right[j++]);
        }
        while (i < left.Count) result.Add(left[i++]);
        while (j < right.Count) result.Add(right[j++]);
        return result;
    }

    private List<CleanedStartup> MergeSortByFounders(List<CleanedStartup> list)
    {
        if (list.Count <= 1) return list;
        int mid = list.Count / 2;
        var left = MergeSortByFounders(list.GetRange(0, mid));
        var right = MergeSortByFounders(list.GetRange(mid, list.Count - mid));
        return MergeByFounders(left, right);
    }
    private List<CleanedStartup> MergeByFounders(List<CleanedStartup> left, List<CleanedStartup> right)
    {
        var result = new List<CleanedStartup>();
        int i = 0, j = 0;
        while (i < left.Count && j < right.Count)
        {
            if (string.Compare(left[i].Founders ?? "", right[j].Founders ?? "", true) <= 0)
                result.Add(left[i++]);
            else
                result.Add(right[j++]);
        }
        while (i < left.Count) result.Add(left[i++]);
        while (j < right.Count) result.Add(right[j++]);
        return result;
    }

    private List<CleanedStartup> MergeSortByInvestmentStage(List<CleanedStartup> list)
    {
        if (list.Count <= 1) return list;
        int mid = list.Count / 2;
        var left = MergeSortByInvestmentStage(list.GetRange(0, mid));
        var right = MergeSortByInvestmentStage(list.GetRange(mid, list.Count - mid));
        return MergeByInvestmentStage(left, right);
    }
    private List<CleanedStartup> MergeByInvestmentStage(List<CleanedStartup> left, List<CleanedStartup> right)
    {
        var result = new List<CleanedStartup>();
        int i = 0, j = 0;
        while (i < left.Count && j < right.Count)
        {
            if (string.Compare(left[i].InvestmentStage ?? "", right[j].InvestmentStage ?? "", true) <= 0)
                result.Add(left[i++]);
            else
                result.Add(right[j++]);
        }
        while (i < left.Count) result.Add(left[i++]);
        while (j < right.Count) result.Add(right[j++]);
        return result;
    }

    private List<CleanedStartup> MergeSortByStartingYear(List<CleanedStartup> list)
    {
        if (list.Count <= 1) return list;
        int mid = list.Count / 2;
        var left = MergeSortByStartingYear(list.GetRange(0, mid));
        var right = MergeSortByStartingYear(list.GetRange(mid, list.Count - mid));
        return MergeByStartingYear(left, right);
    }
    private List<CleanedStartup> MergeByStartingYear(List<CleanedStartup> left, List<CleanedStartup> right)
    {
        var result = new List<CleanedStartup>();
        int i = 0, j = 0;
        while (i < left.Count && j < right.Count)
        {
            int leftYear = int.TryParse(left[i].StartingYear, out int ly) ? ly : int.MaxValue;
            int rightYear = int.TryParse(right[j].StartingYear, out int ry) ? ry : int.MaxValue;

            if (leftYear <= rightYear)
                result.Add(left[i++]);
            else
                result.Add(right[j++]);
        }
        while (i < left.Count) result.Add(left[i++]);
        while (j < right.Count) result.Add(right[j++]);
        return result;
    }
}
