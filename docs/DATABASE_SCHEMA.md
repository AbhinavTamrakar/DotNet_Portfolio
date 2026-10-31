# Database Schema

**Status: design only. No tables or migrations exist yet (Phase 3 will create them).**

Database: **MySQL Server 9.7.1**
Schema name: `personal_portfolio`
Engine: InnoDB
Charset: `utf8mb4` / `utf8mb4_0900_ai_ci`
ORM: EF Core 10.0.12 with Oracle `MySql.EntityFrameworkCore` 10.0.12

---

## Conventions

- Primary keys: `BIGINT` auto-increment (`Id`).
- Timestamps stored in **UTC** (`DATETIME`), created/updated set by the application.
- All slugs unique and indexed; generated and validated server-side.
- Foreign keys enforced; delete behavior chosen per relationship (cascade for owned
  child rows such as project images, restrict for referenced lookup data).
- Indexes on slugs, publication flags, timestamps and foreign keys used in filters.
- Uploaded binaries are **never** stored in MySQL. Only URLs and metadata are stored;
  files live in object storage / local storage outside the source tree.
- Schema changes only through EF Core migrations. No manual production edits.
- MySQL runs with `lower_case_table_names=1`; identifiers are case-insensitive here.

---

## Tables

### Identity (ASP.NET Core Identity standard tables)

`AspNetUsers`, `AspNetRoles`, `AspNetUserRoles`, `AspNetUserClaims`, `AspNetRoleClaims`,
`AspNetUserLogins`, `AspNetUserTokens`.

One administrator is seeded securely (no hardcoded password). Roles are optional for the
single-owner model; no multi-user SaaS layer.

### Profiles

| Column | Type |
|---|---|
| Id | BIGINT PK |
| FullName | VARCHAR(150) |
| ProfessionalTitle | VARCHAR(200) |
| ShortBio | TEXT |
| Biography | LONGTEXT |
| Email | VARCHAR(255) |
| Location | VARCHAR(150) |
| ProfileImageUrl | VARCHAR(1000) |
| CvFileUrl | VARCHAR(1000) |
| CreatedAt / UpdatedAt | DATETIME (UTC) |

One active profile record initially.

### Projects

| Column | Type |
|---|---|
| Id | BIGINT PK |
| Title | VARCHAR(200) |
| Slug | VARCHAR(220) UNIQUE |
| ShortDescription | TEXT |
| Description | LONGTEXT |
| ThumbnailUrl | VARCHAR(1000) |
| GithubUrl | VARCHAR(1000) NULL |
| LiveDemoUrl | VARCHAR(1000) NULL |
| Category | VARCHAR(100) |
| IsFeatured | BOOLEAN |
| IsPublished | BOOLEAN |
| DisplayOrder | INT |
| CreatedAt / UpdatedAt | DATETIME (UTC) |

Indexes: `Slug` (unique), `IsPublished`, `IsFeatured`, `DisplayOrder`.

### ProjectImages

| Column | Type |
|---|---|
| Id | BIGINT PK |
| ProjectId | BIGINT FK → Projects (cascade) |
| ImageUrl | VARCHAR(1000) |
| AltText | VARCHAR(255) |
| DisplayOrder | INT |

One project → many images.

### Technologies

| Column | Type |
|---|---|
| Id | BIGINT PK |
| Name | VARCHAR(100) |
| Slug | VARCHAR(120) UNIQUE |
| IconUrl | VARCHAR(1000) NULL |

### ProjectTechnologies

| Column | Type |
|---|---|
| ProjectId | BIGINT FK → Projects (cascade) |
| TechnologyId | BIGINT FK → Technologies (restrict) |

Composite PK `(ProjectId, TechnologyId)`. Many-to-many.

### Skills

| Column | Type |
|---|---|
| Id | BIGINT PK |
| Name | VARCHAR(120) |
| Category | VARCHAR(100) |
| IconUrl | VARCHAR(1000) NULL |
| ProficiencyLevel | INT NULL |
| DisplayOrder | INT |

No proficiency percentages unless meaningful and defensible.

### Experiences

| Column | Type |
|---|---|
| Id | BIGINT PK |
| CompanyName | VARCHAR(200) |
| Position | VARCHAR(200) |
| EmploymentType | VARCHAR(100) |
| Description | LONGTEXT |
| StartDate | DATE |
| EndDate | DATE NULL |
| IsCurrent | BOOLEAN |
| DisplayOrder | INT |

### Education

| Column | Type |
|---|---|
| Id | BIGINT PK |
| Institution | VARCHAR(200) |
| Qualification | VARCHAR(200) |
| FieldOfStudy | VARCHAR(200) |
| Description | TEXT |
| StartDate | DATE |
| EndDate | DATE NULL |
| DisplayOrder | INT |

### Certifications

| Column | Type |
|---|---|
| Id | BIGINT PK |
| Name | VARCHAR(200) |
| IssuingOrganization | VARCHAR(200) |
| CredentialId | VARCHAR(200) NULL |
| CredentialUrl | VARCHAR(1000) NULL |
| CertificateUrl | VARCHAR(1000) NULL |
| IssueDate | DATE NULL |
| ExpirationDate | DATE NULL |

### Blogs

| Column | Type |
|---|---|
| Id | BIGINT PK |
| Title | VARCHAR(250) |
| Slug | VARCHAR(270) UNIQUE |
| Excerpt | TEXT |
| Content | LONGTEXT |
| FeaturedImageUrl | VARCHAR(1000) |
| SeoTitle | VARCHAR(250) NULL |
| SeoDescription | VARCHAR(500) NULL |
| Status | VARCHAR(30) (Draft/Published) |
| PublishedAt | DATETIME NULL |
| CreatedAt / UpdatedAt | DATETIME (UTC) |

Indexes: `Slug` (unique), `Status`, `PublishedAt`.

### BlogCategories

| Column | Type |
|---|---|
| Id | BIGINT PK |
| Name | VARCHAR(120) |
| Slug | VARCHAR(140) UNIQUE |

### BlogTags

| Column | Type |
|---|---|
| Id | BIGINT PK |
| Name | VARCHAR(120) |
| Slug | VARCHAR(140) UNIQUE |

### BlogPostCategories

| Column | Type |
|---|---|
| BlogId | BIGINT FK → Blogs (cascade) |
| CategoryId | BIGINT FK → BlogCategories (restrict) |

Composite PK `(BlogId, CategoryId)`.

### BlogPostTags

| Column | Type |
|---|---|
| BlogId | BIGINT FK → Blogs (cascade) |
| TagId | BIGINT FK → BlogTags (restrict) |

Composite PK `(BlogId, TagId)`.

### Inquiries

| Column | Type |
|---|---|
| Id | BIGINT PK |
| Name | VARCHAR(150) |
| Email | VARCHAR(255) |
| Subject | VARCHAR(250) |
| Message | TEXT |
| IsRead | BOOLEAN |
| CreatedAt | DATETIME (UTC) |

Indexes: `IsRead`, `CreatedAt`.

### SocialLinks

| Column | Type |
|---|---|
| Id | BIGINT PK |
| Platform | VARCHAR(100) |
| Url | VARCHAR(1000) |
| DisplayOrder | INT |
| IsVisible | BOOLEAN |

### SiteSettings

| Column | Type |
|---|---|
| Id | BIGINT PK |
| SettingKey | VARCHAR(150) UNIQUE |
| SettingValue | TEXT NULL |
| UpdatedAt | DATETIME (UTC) |

---

## Relationship summary

```text
Profiles (1)

Projects 1 ─── * ProjectImages
Projects * ─── * Technologies      (ProjectTechnologies)

Blogs 1 ─── * BlogPostCategories ─── 1 BlogCategories
Blogs 1 ─── * BlogPostTags       ─── 1 BlogTags

Inquiries, Skills, Experiences, Education,
Certifications, SocialLinks, SiteSettings   (independent)

AspNetUsers * ─── * AspNetRoles    (standard Identity tables)
```

## Migration history

| Migration | Status |
|---|---|
| `InitialCreate` | not created yet — planned for Phase 3 |
