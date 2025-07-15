using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyTraining1101Demo.MultiTenancy.HostDashboard.Dto;

namespace MyTraining1101Demo.MultiTenancy.HostDashboard
{
    public interface IIncomeStatisticsService
    {
        Task<List<IncomeStastistic>> GetIncomeStatisticsData(DateTime startDate, DateTime endDate,
            ChartDateInterval dateInterval);
    }
}