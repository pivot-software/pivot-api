-- create_workspace_table.sql
CREATE TABLE workspace
(
    id            SERIAL PRIMARY KEY ,
    business_name VARCHAR(500) NOT NULL,
    business_logo VARCHAR(500),
    primary_color VARCHAR(500),
    template_mode CHAR(1)      NOT NULL DEFAULT '0',
    admin_id      INT,
    UNIQUE (business_name)
);

ALTER TABLE workspace ADD FOREIGN KEY(admin_id) REFERENCES users (id)
