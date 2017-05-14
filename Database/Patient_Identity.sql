create table patient_identity(
patient_id int references Patient_Master(patient_id),
aadhar_no varchar(20),
passport_id varchar(20),
pan_id varchar(20),
voter_id varchar(20),
emp_id varchar(20)
);