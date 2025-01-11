﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer")
                        .HasColumnName("access_failed_count");

                    b.Property<string>("AvatarImageFilepath")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("avatar_image_filepath");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("city");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("text")
                        .HasColumnName("concurrency_stamp");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean")
                        .HasColumnName("email_confirmed");

                    b.Property<string>("Fio")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("fio");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean")
                        .HasColumnName("lockout_enabled");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("lockout_end");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("text")
                        .HasColumnName("normalized_email");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("text")
                        .HasColumnName("normalized_user_name");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password_hash");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("phone");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text")
                        .HasColumnName("phone_number");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean")
                        .HasColumnName("phone_number_confirmed");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid")
                        .HasColumnName("role_id");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text")
                        .HasColumnName("security_stamp");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean")
                        .HasColumnName("two_factor_enabled");

                    b.Property<string>("UserName")
                        .HasColumnType("text")
                        .HasColumnName("user_name");

                    b.Property<string>("UserStatus")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user_status");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_users_role_id");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.DynamicFieldType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.HasKey("Id")
                        .HasName("pk_dynamic_field_types");

                    b.ToTable("dynamic_field_types", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("916a0b89-3162-4753-b431-96121226e7e7"),
                            Title = "string"
                        },
                        new
                        {
                            Id = new Guid("9a9c32a1-96fe-4593-9360-571b7a3fb754"),
                            Title = "number"
                        });
                });

            modelBuilder.Entity("Domain.Entities.EntryFieldValue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("DynamicFieldId")
                        .HasColumnType("uuid")
                        .HasColumnName("dynamic_field_id");

                    b.Property<Guid>("EventSignupEntryId")
                        .HasColumnType("uuid")
                        .HasColumnName("event_signup_entry_id");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.HasKey("Id")
                        .HasName("pk_entry_field_values");

                    b.HasIndex("DynamicFieldId")
                        .HasDatabaseName("ix_entry_field_values_dynamic_field_id");

                    b.HasIndex("EventSignupEntryId")
                        .HasDatabaseName("ix_entry_field_values_event_signup_entry_id");

                    b.ToTable("entry_field_values", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.EventSignupEntry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uuid")
                        .HasColumnName("event_id");

                    b.Property<string>("Phone")
                        .HasColumnType("text")
                        .HasColumnName("phone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_event_signup_entries");

                    b.HasIndex("EventId")
                        .HasDatabaseName("ix_event_signup_entries_event_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_event_signup_entries_user_id");

                    b.ToTable("event_signup_entries", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.EventSignupForm", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uuid")
                        .HasColumnName("event_id");

                    b.Property<bool>("IsEmailRequired")
                        .HasColumnType("boolean")
                        .HasColumnName("is_email_required");

                    b.Property<bool>("IsPhoneRequired")
                        .HasColumnType("boolean")
                        .HasColumnName("is_phone_required");

                    b.HasKey("Id")
                        .HasName("pk_event_signup_forms");

                    b.HasIndex("EventId")
                        .HasDatabaseName("ix_event_signup_forms_event_id");

                    b.ToTable("event_signup_forms", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.FormDynamicField", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("EventFormId")
                        .HasColumnType("uuid")
                        .HasColumnName("event_form_id");

                    b.Property<Guid>("FieldTypeId")
                        .HasColumnType("uuid")
                        .HasColumnName("field_type_id");

                    b.Property<bool>("IsRequired")
                        .HasColumnType("boolean")
                        .HasColumnName("is_required");

                    b.Property<int>("MaxSymbols")
                        .HasColumnType("integer")
                        .HasColumnName("max_symbols");

                    b.Property<string>("MaxValue")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("max_value");

                    b.Property<string>("MinValue")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("min_value");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.HasKey("Id")
                        .HasName("pk_form_dynamic_fields");

                    b.HasIndex("EventFormId")
                        .HasDatabaseName("ix_form_dynamic_fields_event_form_id");

                    b.HasIndex("FieldTypeId")
                        .HasDatabaseName("ix_form_dynamic_fields_field_type_id");

                    b.ToTable("form_dynamic_fields", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.OrganizerContacts", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uuid")
                        .HasColumnName("event_id");

                    b.Property<string>("Fio")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("fio");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("phone");

                    b.Property<string>("Telegram")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("telegram");

                    b.HasKey("Id")
                        .HasName("pk_organizer_contacts");

                    b.HasIndex("EventId")
                        .HasDatabaseName("ix_organizer_contacts_event_id");

                    b.ToTable("organizer_contacts", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.UserEvent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Address")
                        .HasColumnType("text")
                        .HasColumnName("address");

                    b.Property<string>("BannerImageFilepath")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("banner_image_filepath");

                    b.Property<string>("City")
                        .HasColumnType("text")
                        .HasColumnName("city");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("creation_date");

                    b.Property<Guid>("CreatorUserId")
                        .HasColumnType("uuid")
                        .HasColumnName("creator_user_id");

                    b.Property<DateTime>("DateStart")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_start");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<bool>("IsOnline")
                        .HasColumnType("boolean")
                        .HasColumnName("is_online");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("boolean")
                        .HasColumnName("is_public");

                    b.Property<bool>("IsSignupOpened")
                        .HasColumnType("boolean")
                        .HasColumnName("is_signup_opened");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.HasKey("Id")
                        .HasName("pk_users_events");

                    b.HasIndex("CreatorUserId")
                        .HasDatabaseName("ix_users_events_creator_user_id");

                    b.ToTable("users_events", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("text")
                        .HasColumnName("concurrency_stamp");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("text")
                        .HasColumnName("normalized_name");

                    b.HasKey("Id")
                        .HasName("pk_roles");

                    b.ToTable("roles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text")
                        .HasColumnName("claim_type");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text")
                        .HasColumnName("claim_value");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid")
                        .HasColumnName("role_id");

                    b.HasKey("Id")
                        .HasName("pk_role_claims");

                    b.ToTable("role_claims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text")
                        .HasColumnName("claim_type");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text")
                        .HasColumnName("claim_value");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_user_claims");

                    b.ToTable("user_claims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<string>("LoginProvider")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("login_provider");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text")
                        .HasColumnName("provider_display_name");

                    b.Property<string>("ProviderKey")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("provider_key");

                    b.HasKey("UserId")
                        .HasName("pk_user_logins");

                    b.ToTable("user_logins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid")
                        .HasColumnName("role_id");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("RoleId", "UserId")
                        .HasName("pk_user_roles");

                    b.ToTable("user_roles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<string>("LoginProvider")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("login_provider");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Value")
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.HasKey("UserId")
                        .HasName("pk_user_tokens");

                    b.ToTable("user_tokens", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.ApplicationUser", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_users_roles_role_id");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Domain.Entities.EntryFieldValue", b =>
                {
                    b.HasOne("Domain.Entities.FormDynamicField", "DynamicField")
                        .WithMany()
                        .HasForeignKey("DynamicFieldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_entry_field_values_form_dynamic_fields_dynamic_field_id");

                    b.HasOne("Domain.Entities.EventSignupEntry", "EventSignupEntry")
                        .WithMany()
                        .HasForeignKey("EventSignupEntryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_entry_field_values_event_signup_entries_event_signup_entry_");

                    b.Navigation("DynamicField");

                    b.Navigation("EventSignupEntry");
                });

            modelBuilder.Entity("Domain.Entities.EventSignupEntry", b =>
                {
                    b.HasOne("Domain.Entities.UserEvent", "Event")
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_event_signup_entries_users_events_event_id");

                    b.HasOne("Domain.Entities.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_event_signup_entries_users_user_id");

                    b.Navigation("Event");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.EventSignupForm", b =>
                {
                    b.HasOne("Domain.Entities.UserEvent", "Event")
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_event_signup_forms_users_events_event_id");

                    b.Navigation("Event");
                });

            modelBuilder.Entity("Domain.Entities.FormDynamicField", b =>
                {
                    b.HasOne("Domain.Entities.EventSignupForm", "SignupForm")
                        .WithMany()
                        .HasForeignKey("EventFormId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_form_dynamic_fields_event_signup_forms_event_form_id");

                    b.HasOne("Domain.Entities.DynamicFieldType", "FieldType")
                        .WithMany()
                        .HasForeignKey("FieldTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_form_dynamic_fields_dynamic_field_types_field_type_id");

                    b.Navigation("FieldType");

                    b.Navigation("SignupForm");
                });

            modelBuilder.Entity("Domain.Entities.OrganizerContacts", b =>
                {
                    b.HasOne("Domain.Entities.UserEvent", "Event")
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_organizer_contacts_users_events_event_id");

                    b.Navigation("Event");
                });

            modelBuilder.Entity("Domain.Entities.UserEvent", b =>
                {
                    b.HasOne("Domain.Entities.ApplicationUser", "CreatorUser")
                        .WithMany()
                        .HasForeignKey("CreatorUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_users_events_users_creator_user_id");

                    b.Navigation("CreatorUser");
                });
#pragma warning restore 612, 618
        }
    }
}