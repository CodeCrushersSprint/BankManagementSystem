﻿namespace BMSWebApi.Model
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int AccountId {  get; set; }
        public decimal Amount {  get; set; }
        public string TransactionType {  get; set; }
        public DateTime TransactionDate { get; set; }

        public virtual Account Account { get; set; }
    }
}