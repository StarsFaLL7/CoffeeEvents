﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dynamic_field_types",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_dynamic_field_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "refresh_tokens",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    token = table.Column<string>(type: "text", nullable: false),
                    expiry_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_refresh_tokens", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "revoked_access_tokens",
                columns: table => new
                {
                    jti = table.Column<Guid>(type: "uuid", nullable: false),
                    expiration_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_revoked_access_tokens", x => x.jti);
                });

            migrationBuilder.CreateTable(
                name: "users_roles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    can_edit_others_events = table.Column<bool>(type: "boolean", nullable: false),
                    is_admin = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    phone = table.Column<string>(type: "text", nullable: false),
                    fio = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: false),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_status = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    city = table.Column<string>(type: "text", nullable: false),
                    avatar_image_filepath = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                    table.ForeignKey(
                        name: "fk_users_users_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "users_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users_events",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creator_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_public = table.Column<bool>(type: "boolean", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    banner_image_filepath = table.Column<string>(type: "text", nullable: false),
                    is_online = table.Column<bool>(type: "boolean", nullable: false),
                    is_signup_opened = table.Column<bool>(type: "boolean", nullable: false),
                    city = table.Column<string>(type: "text", nullable: true),
                    address = table.Column<string>(type: "text", nullable: true),
                    date_start = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_end = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users_events", x => x.id);
                    table.ForeignKey(
                        name: "fk_users_events_users_creator_user_id",
                        column: x => x.creator_user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "event_signup_forms",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_phone_required = table.Column<bool>(type: "boolean", nullable: false),
                    is_email_required = table.Column<bool>(type: "boolean", nullable: false),
                    is_fio_required = table.Column<bool>(type: "boolean", nullable: false),
                    event_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_event_signup_forms", x => x.id);
                    table.ForeignKey(
                        name: "fk_event_signup_forms_users_events_event_id",
                        column: x => x.event_id,
                        principalTable: "users_events",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "event_signup_windows",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    event_id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    time = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    max_visitors = table.Column<int>(type: "integer", nullable: false),
                    tickets_left = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_event_signup_windows", x => x.id);
                    table.ForeignKey(
                        name: "fk_event_signup_windows_users_events_event_id",
                        column: x => x.event_id,
                        principalTable: "users_events",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "organizer_contacts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    event_id = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    fio = table.Column<string>(type: "text", nullable: false),
                    phone = table.Column<string>(type: "text", nullable: false),
                    telegram = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organizer_contacts", x => x.id);
                    table.ForeignKey(
                        name: "fk_organizer_contacts_users_events_event_id",
                        column: x => x.event_id,
                        principalTable: "users_events",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "form_dynamic_fields",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    is_required = table.Column<bool>(type: "boolean", nullable: false),
                    field_type_id = table.Column<Guid>(type: "uuid", nullable: false),
                    max_symbols = table.Column<int>(type: "integer", nullable: false),
                    min_value = table.Column<string>(type: "text", nullable: false),
                    max_value = table.Column<string>(type: "text", nullable: false),
                    event_form_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_form_dynamic_fields", x => x.id);
                    table.ForeignKey(
                        name: "fk_form_dynamic_fields_dynamic_field_types_field_type_id",
                        column: x => x.field_type_id,
                        principalTable: "dynamic_field_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_form_dynamic_fields_event_signup_forms_event_form_id",
                        column: x => x.event_form_id,
                        principalTable: "event_signup_forms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "event_signup_entries",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    signup_window_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    phone = table.Column<string>(type: "text", nullable: true),
                    fio = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_event_signup_entries", x => x.id);
                    table.ForeignKey(
                        name: "fk_event_signup_entries_event_signup_windows_signup_window_id",
                        column: x => x.signup_window_id,
                        principalTable: "event_signup_windows",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_event_signup_entries_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "entry_field_values",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    event_signup_entry_id = table.Column<Guid>(type: "uuid", nullable: false),
                    dynamic_field_id = table.Column<Guid>(type: "uuid", nullable: false),
                    value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_entry_field_values", x => x.id);
                    table.ForeignKey(
                        name: "fk_entry_field_values_event_signup_entries_event_signup_entry_",
                        column: x => x.event_signup_entry_id,
                        principalTable: "event_signup_entries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_entry_field_values_form_dynamic_fields_dynamic_field_id",
                        column: x => x.dynamic_field_id,
                        principalTable: "form_dynamic_fields",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "dynamic_field_types",
                columns: new[] { "id", "title" },
                values: new object[,]
                {
                    { new Guid("9f99c974-94cc-4444-bf63-26fb99c6d56f"), "string" },
                    { new Guid("f420022e-ebb6-4f49-ac50-80a3c281f91b"), "number" }
                });

            migrationBuilder.InsertData(
                table: "users_roles",
                columns: new[] { "id", "can_edit_others_events", "is_admin", "title" },
                values: new object[,]
                {
                    { new Guid("1f103d44-5eb4-4667-bc6c-b05091c35267"), false, false, "User" },
                    { new Guid("667f7e21-aeb8-4c3b-8d2e-f05769784643"), true, false, "Moderator" },
                    { new Guid("86f62fae-6821-48ca-acd0-1a29ab6d68a6"), true, true, "Admin" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_entry_field_values_dynamic_field_id",
                table: "entry_field_values",
                column: "dynamic_field_id");

            migrationBuilder.CreateIndex(
                name: "ix_entry_field_values_event_signup_entry_id",
                table: "entry_field_values",
                column: "event_signup_entry_id");

            migrationBuilder.CreateIndex(
                name: "ix_event_signup_entries_signup_window_id",
                table: "event_signup_entries",
                column: "signup_window_id");

            migrationBuilder.CreateIndex(
                name: "ix_event_signup_entries_user_id",
                table: "event_signup_entries",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_event_signup_forms_event_id",
                table: "event_signup_forms",
                column: "event_id");

            migrationBuilder.CreateIndex(
                name: "ix_event_signup_windows_event_id",
                table: "event_signup_windows",
                column: "event_id");

            migrationBuilder.CreateIndex(
                name: "ix_form_dynamic_fields_event_form_id",
                table: "form_dynamic_fields",
                column: "event_form_id");

            migrationBuilder.CreateIndex(
                name: "ix_form_dynamic_fields_field_type_id",
                table: "form_dynamic_fields",
                column: "field_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_organizer_contacts_event_id",
                table: "organizer_contacts",
                column: "event_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_role_id",
                table: "users",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_events_creator_user_id",
                table: "users_events",
                column: "creator_user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "entry_field_values");

            migrationBuilder.DropTable(
                name: "organizer_contacts");

            migrationBuilder.DropTable(
                name: "refresh_tokens");

            migrationBuilder.DropTable(
                name: "revoked_access_tokens");

            migrationBuilder.DropTable(
                name: "event_signup_entries");

            migrationBuilder.DropTable(
                name: "form_dynamic_fields");

            migrationBuilder.DropTable(
                name: "event_signup_windows");

            migrationBuilder.DropTable(
                name: "dynamic_field_types");

            migrationBuilder.DropTable(
                name: "event_signup_forms");

            migrationBuilder.DropTable(
                name: "users_events");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "users_roles");
        }
    }
}
