using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRAC1
{
    public interface IReport
    {
        string Generate();
    }

    public class SalesReport : IReport
    {
        public string Generate()
        {
            return "Отчет по продажам";
        }
    }

    public class UserReport : IReport
    {
        public string Generate()
        {
            return "Отчет по пользователям";
        }
    }

    public abstract class ReportDecorator : IReport
    {
        protected IReport _report;

        public ReportDecorator(IReport report)
        {
            _report = report;
        }

        public virtual string Generate()
        {
            return _report.Generate();
        }
    }

    public class DateFilterDecorator : ReportDecorator
    {
        private DateTime _startDate;
        private DateTime _endDate;

        public DateFilterDecorator(IReport report, DateTime startDate, DateTime endDate) : base(report)
        {
            _startDate = startDate;
            _endDate = endDate;
        }

        public override string Generate()
        {
            return $"{_report.Generate()} (с фильтром по датам: с {_startDate.ToShortDateString()} по {_endDate.ToShortDateString()})";
        }
    }

    public class SortingDecorator : ReportDecorator
    {
        private string _sortCriterion;

        public SortingDecorator(IReport report, string sortCriterion) : base(report)
        {
            _sortCriterion = sortCriterion;
        }

        public override string Generate()
        {
            return $"{_report.Generate()} (сортировка по: {_sortCriterion})";
        }
    }

    public class CsvExportDecorator : ReportDecorator
    {
        public CsvExportDecorator(IReport report) : base(report) { }

        public override string Generate()
        {
            return $"{_report.Generate()} (экспорт в CSV)";
        }
    }

    public class PdfExportDecorator : ReportDecorator
    {
        public PdfExportDecorator(IReport report) : base(report) { }

        public override string Generate()
        {
            return $"{_report.Generate()} (экспорт в PDF)";
        }
    }

    public class ReportClient
    {
        public static void Main()
        {
            IReport report = new SalesReport();

            report = new DateFilterDecorator(report, new DateTime(2024, 1, 1), new DateTime(2024, 12, 31));
            report = new SortingDecorator(report, "дате");
            report = new CsvExportDecorator(report);

            Console.WriteLine(report.Generate());

            IReport userReport = new UserReport();
            userReport = new PdfExportDecorator(userReport);
            Console.WriteLine(userReport.Generate());
        }
    }
}
