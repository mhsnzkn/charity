using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Results
{
    public class ResultData<T> : Result
    {
        public T Data { get; set; }
        public ResultData(T data)
        {
            this.Data = data;
        }
        public ResultData()
        {

        }
    }

    public class ResultDataList<T> : Result
    {
        public List<T> Data { get; set; }
        public ResultDataList(List<T> dataList)
        {
            this.Data = dataList;
        }
        public ResultDataList()
        {

        }
    }
}
