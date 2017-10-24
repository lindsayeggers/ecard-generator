drop table cards;
drop table card_templates;

create table card_templates
(
	id			int				identity(1,1),
	name		varchar(255)	not null,
	imageName	varchar(255) 	not null,
	fontColor	varchar(100)	not null,
		
	constraint pk_card_templates primary key (id)
);


create table cards
(
	id			int				identity(1,1),
	template_id	int				not null,
	to_email	varchar(100)	not null,
	to_name		varchar(100)	not null,
	message		varchar(100)	not null,
	from_name	varchar(100)	not null,
	from_email	varchar(100)	not null,

	constraint pk_cards primary key(id),
	constraint fk_cards_card_templates foreign key (template_id) references card_templates(id)
);



insert into cards values (1, 'john@xyz.com', 'John', 'Get well soon!', 'Jack', 'jack@abc.com');

insert into card_templates values ('Three Trolls Card', '3Trolls.jpg', '#ffffff')
insert into card_templates values ('Baby Troll Card', 'Baby.jpg', '#ffffff');
insert into card_templates values ('Bikini Troll Card', 'Bikini.jpg', '#ffffff');
insert into card_templates values ('Circus Troll Card', 'Circus.jpg', '#ffffff');
insert into card_templates values ('Clown Troll Card', 'Clown.jpg', '#ffffff');
insert into card_templates values ('Guitar Troll Card', 'Guitar.jpg', '#ffffff');
insert into card_templates values ('Hamburger Troll Card', 'Hamburger.jpg', '#ffffff');
insert into card_templates values ('Jailbird Troll Card', 'Jail.jpg', '#ffffff');
insert into card_templates values ('Lotto Troll Card', 'Lotto.jpg', '#ffffff');
insert into card_templates values ('Olympics Troll Card', 'Olympics.jpg', '#ffffff');
insert into card_templates values ('Rainbow Troll Card', 'Rainbow.jpg', '#ffffff');
insert into card_templates values ('Sockhop Troll Card', 'SockHop.jpg', '#ffffff');
insert into card_templates values ('Swim Troll Card', 'Swim.jpg', '#ffffff');
insert into card_templates values ('Toga Troll Card', 'Toga.jpg', '#ffffff');
insert into card_templates values ('Workout Troll Card', 'Workout.jpg', '#ffffff');

SELECT * from card_templates;

UPDATE card_templates
SET imageName = 'hamburger.jpg' 
WHERE card_templates.id = 11;

SELECT * FROM card_templates WHERE card_templates.name = 'blue card';


SELECT * FROM card_templates
SELECT * FROM cards;

DELETE FROM cards WHERE cards.id = 1;

SELECT * FROM card_templates WHERE card_templates.id = 1;