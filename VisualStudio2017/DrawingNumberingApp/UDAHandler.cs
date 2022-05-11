/* Copyright 2018 Dawid Dyrcz 
 * See License.txt file 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tekla.Structures.Catalogs;

namespace DrawingNumberingPlugin
{
    internal class UDAHandler
    {
        public UDAHandler()
        {
            GetUDAFromModel();
        }

        private List<Tuple<string, string>> _udaList;

        private void GetUDAFromModel()
        {
            this._udaList = GetAllDrawingsUDA();
        }

        List<Tuple<string, string>> GetAllDrawingsUDA()
        {
            var returnList = new List<Tuple<string, string>>();
            var ct = new CatalogHandler();

            var itemEnumerator = ct.GetUserPropertyItems(CatalogObjectTypeEnum.GA_DRAWING);

            while (itemEnumerator.MoveNext())
            {
                var uda = itemEnumerator.Current.Name;
                var udaLabel = itemEnumerator.Current.GetLabel();
                if (!returnList.Exists(x => x.Item1.Equals(uda, StringComparison.InvariantCulture))) returnList.Add(new Tuple<string, string>(uda, udaLabel));
            }

            itemEnumerator.Reset();
            itemEnumerator = ct.GetUserPropertyItems(CatalogObjectTypeEnum.ASSEMBLY_DRAWING);

            while (itemEnumerator.MoveNext())
            {
                var uda = itemEnumerator.Current.Name;
                var udaLabel = itemEnumerator.Current.GetLabel();
                if (!returnList.Exists(x => x.Item1.Equals(uda, StringComparison.InvariantCulture))) returnList.Add(new Tuple<string, string>(uda, udaLabel));

            }

            itemEnumerator.Reset();
            itemEnumerator = ct.GetUserPropertyItems(CatalogObjectTypeEnum.SINGLE_PART_DRAWING);

            while (itemEnumerator.MoveNext())
            {
                var uda = itemEnumerator.Current.Name;
                var udaLabel = itemEnumerator.Current.GetLabel();
                if (!returnList.Exists(x => x.Item1.Equals(uda, StringComparison.InvariantCulture))) returnList.Add(new Tuple<string, string>(uda, udaLabel));

            }

            itemEnumerator.Reset();
            itemEnumerator = ct.GetUserPropertyItems(CatalogObjectTypeEnum.CAST_UNIT_DRAWING);

            while (itemEnumerator.MoveNext())
            {
                var uda = itemEnumerator.Current.Name;
                var udaLabel = itemEnumerator.Current.GetLabel();
                if (!returnList.Exists(x => x.Item1.Equals(uda, StringComparison.InvariantCulture))) returnList.Add(new Tuple<string, string>(uda, udaLabel));

            }

            itemEnumerator.Reset();
            itemEnumerator = ct.GetUserPropertyItems(CatalogObjectTypeEnum.MULTI_DRAWING);

            while (itemEnumerator.MoveNext())
            {
                var uda = itemEnumerator.Current.Name;
                var udaLabel = itemEnumerator.Current.GetLabel();
                if (!returnList.Exists(x => x.Item1.Equals(uda, StringComparison.InvariantCulture))) returnList.Add(new Tuple<string, string>(uda, udaLabel));

            }

            returnList.Sort((x, y) => string.Compare(x.Item1, y.Item1, StringComparison.InvariantCulture));
            return returnList;
        }

        public List<string> GetAllUDA()
        {
            var returnList = new List<string>();

            foreach (var currentTuple in _udaList)
            {
                returnList.Add(currentTuple.Item1);
            }

            return returnList;
        }

        public List<string> GetAllUDALabels()
        {
            var returnList = new List<string>();

            foreach (var currentTuple in _udaList)
            {
                returnList.Add(currentTuple.Item2);
            }

            return returnList;
        }

        public string GetUDAByIndex(int index)
        {
            if (index >= 0 & index < _udaList.Count)
                return _udaList[index].Item1;
            else return "";
        }

        public string GetUDALabelByIndex(int index)
        {
            if (index >= 0 & index < _udaList.Count)
                return _udaList[index].Item2;
            else return "";
        }

    }
}
