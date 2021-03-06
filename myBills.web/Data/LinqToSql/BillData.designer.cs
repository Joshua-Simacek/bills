﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace myBills.web.Data.LinqToSql
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="Omega")]
	public partial class BillDataDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public BillDataDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["OmegaConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public BillDataDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public BillDataDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public BillDataDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public BillDataDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<bill> bills
		{
			get
			{
				return this.GetTable<bill>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.bills")]
	public partial class bill
	{
		
		private string _name;
		
		private System.Nullable<decimal> _amount;
		
		private System.Nullable<char> _pay_type;
		
		private System.Nullable<byte> _day_of_month;
		
		private System.Nullable<System.DateTime> _seed_date;
		
		private System.Nullable<byte> _day_of_week;
		
		private System.Nullable<char> _pay_interval;
		
		public bill()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_name", DbType="VarChar(64)")]
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				if ((this._name != value))
				{
					this._name = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_amount", DbType="Decimal(18,4)")]
		public System.Nullable<decimal> amount
		{
			get
			{
				return this._amount;
			}
			set
			{
				if ((this._amount != value))
				{
					this._amount = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_pay_type", DbType="Char(1)")]
		public System.Nullable<char> pay_type
		{
			get
			{
				return this._pay_type;
			}
			set
			{
				if ((this._pay_type != value))
				{
					this._pay_type = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_day_of_month", DbType="TinyInt")]
		public System.Nullable<byte> day_of_month
		{
			get
			{
				return this._day_of_month;
			}
			set
			{
				if ((this._day_of_month != value))
				{
					this._day_of_month = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_seed_date", DbType="Date")]
		public System.Nullable<System.DateTime> seed_date
		{
			get
			{
				return this._seed_date;
			}
			set
			{
				if ((this._seed_date != value))
				{
					this._seed_date = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_day_of_week", DbType="TinyInt")]
		public System.Nullable<byte> day_of_week
		{
			get
			{
				return this._day_of_week;
			}
			set
			{
				if ((this._day_of_week != value))
				{
					this._day_of_week = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_pay_interval", DbType="Char(1)")]
		public System.Nullable<char> pay_interval
		{
			get
			{
				return this._pay_interval;
			}
			set
			{
				if ((this._pay_interval != value))
				{
					this._pay_interval = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
