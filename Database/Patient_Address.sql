create table patient_Contact(
patient_id int REFERENCES Patient_Master(patient_id), 
t_address varchar(255),
t_city varchar(255) not null,
t_country varchar(255) not null,
t_state varchar(255) not null,
t_district varchar(255) not null,
t_pin bigint not null,
p_address varchar(255),
p_city varchar(255) not null,
p_country varchar(255) not null,
p_state varchar(255) not null,
p_district varchar(255) not null,
p_pin bigint not null,
mob_no varchar(10) not null,
alt_mob_no varchar(10),
tel_no varchar(10),
email varchar(255),
alt_email varchar(255)
);