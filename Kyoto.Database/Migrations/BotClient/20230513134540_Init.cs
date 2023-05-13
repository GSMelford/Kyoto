using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kyoto.Database.Migrations.BotClient
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Commands",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SessionId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ExternalUserId = table.Column<long>(type: "bigint", nullable: false),
                    Step = table.Column<int>(type: "integer", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false),
                    AdditionalData = table.Column<string>(type: "text", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuPanels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuPanels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemStatuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TemplateMessageTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateMessageTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PreparedMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PostEventId = table.Column<Guid>(type: "uuid", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreparedMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreparedMessages_EventMessages_PostEventId",
                        column: x => x.PostEventId,
                        principalTable: "EventMessages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MenuButtons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    MenuPanelId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsCommand = table.Column<bool>(type: "boolean", nullable: false),
                    IsEnable = table.Column<bool>(type: "boolean", nullable: false),
                    Index = table.Column<int>(type: "integer", nullable: false),
                    Line = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuButtons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuButtons_MenuPanels_MenuPanelId",
                        column: x => x.MenuPanelId,
                        principalTable: "MenuPanels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TemplateMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TemplateMessageTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemplateMessages_TemplateMessageTypes_TemplateMessageTypeId",
                        column: x => x.TemplateMessageTypeId,
                        principalTable: "TemplateMessageTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExternalUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    PrivateId = table.Column<long>(type: "bigint", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    MenuPanelId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExternalUsers_MenuPanels_MenuPanelId",
                        column: x => x.MenuPanelId,
                        principalTable: "MenuPanels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ExternalUsers_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExternalUsers_MenuPanelId",
                table: "ExternalUsers",
                column: "MenuPanelId");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalUsers_UserId",
                table: "ExternalUsers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MenuButtons_MenuPanelId",
                table: "MenuButtons",
                column: "MenuPanelId");

            migrationBuilder.CreateIndex(
                name: "IX_PreparedMessages_PostEventId",
                table: "PreparedMessages",
                column: "PostEventId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateMessages_TemplateMessageTypeId",
                table: "TemplateMessages",
                column: "TemplateMessageTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Commands");

            migrationBuilder.DropTable(
                name: "ExternalUsers");

            migrationBuilder.DropTable(
                name: "MenuButtons");

            migrationBuilder.DropTable(
                name: "PreparedMessages");

            migrationBuilder.DropTable(
                name: "SystemStatuses");

            migrationBuilder.DropTable(
                name: "TemplateMessages");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "MenuPanels");

            migrationBuilder.DropTable(
                name: "EventMessages");

            migrationBuilder.DropTable(
                name: "TemplateMessageTypes");
        }
    }
}
