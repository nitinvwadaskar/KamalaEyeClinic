create table patient_insurance(
patient_id int references Patient_Master(patient_id),
conpany_name varchar(255),
comp_address varchar(255),
emp_id varchar(20),
insurance_comp_name varchar(255),
insurance_no varchar(20)
);