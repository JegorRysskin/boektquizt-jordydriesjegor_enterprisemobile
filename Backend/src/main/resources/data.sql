INSERT INTO `role` (`id`, `description`, `name`) VALUES
(0, 'Team', 'USER'),
(1, 'Admin', 'ADMIN'),
(2, 'Engineer', 'ENGINEER');


INSERT INTO `team` (`id`, `enabled`, `name`, `scores`) VALUES
(1, b'0', 'Team1', 0),
(2, b'0', 'Team2', 0),
(3, b'0', 'Team3', 0);

INSERT INTO `user` (`id`, `password`, `username`, `team_id`) VALUES
(1, '$2a$10$sXvem93Fd4nh9VDZFWcwBe5j9j43mJDTDjR6G3Qiv1/6nTIJGBgSW', 'admin', NULL),
(2, '$2a$10$8pzN69VGF46Ab9BeqWskEOqObvY7u6NsLY993otheCOH2TGwkMSyG', 'engineer', NULL),
(3, '$2a$10$OC/nkRkqVqbHWXYsmWbxD.OrOeaupHPWpZzj0GUFzFkckM/IcKe12', 'Team1', 1),
(4, '$2a$10$lGj1Em/wpDHdBL0c1QdRmuqhR13lcYlRxNpP10TEOEbZ/C.iPB4j2', 'Team2', 2),
(5, '$2a$10$AWDQ9WEtuBHacP7Abp6wgOd8fKWwau/ma8j.G2rU65HhJFgrtiVKu', 'Team3', 3);


INSERT INTO `user_roles` (`user_id`, `role_id`) VALUES
(1, 1),
(2, 2),
(3, 0),
(4, 0),
(5, 0);