using System.Collections;
using System.Collections.Generic;

namespace DraughtLeague.Untappd.Models.Beer.Checkins
{
    public class CheckinCollection  {
        public int Count { get; set; }
        public List<Checkin> Items { get; set; }
        //public bool IsReadOnly { get; }
        
        //public void Add(Checkin item) {
        //    Checkins.Add(item);
        //}

        //public void Clear()
        //{
        //    Checkins.Clear();
        //}

        //public bool Contains(Checkin item) {
        //    return Checkins.Contains(item);
        //}

        //public void CopyTo(Checkin[] array, int arrayIndex)
        //{
        //    Checkins.CopyTo(array, arrayIndex);
        //}

        //public bool Remove(Checkin item)
        //{
        //    return Checkins.Remove(item);
        //}

        public IEnumerator<Checkin> GetEnumerator() => Items.GetEnumerator();

    }
}
