﻿namespace BMSWebApi.Model
{
    public class Role
    {
        public int RoleId {  get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
    }
}