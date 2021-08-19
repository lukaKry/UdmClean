﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace UdmClean.Persistence.Migrations
{
    public partial class AddedEmployeeIdToLeaveRequestTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RequestingEmployeeId",
                table: "LeaveRequests",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestingEmployeeId",
                table: "LeaveRequests");
        }
    }
}
