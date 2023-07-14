create table discounts
(
    Id          serial primary key,
    UserId      varchar(200) unique not null,
    Rate        smallint            not null,
    Code        varchar(50)         not null,
    CreatedDate timestamp           not null default current_timestamp
)