package enterpriseAndMobile.service;


import enterpriseAndMobile.model.Role;
import enterpriseAndMobile.repository.RoleRepository;
import org.springframework.stereotype.Service;

import java.util.HashSet;
import java.util.Set;


@Service
public class RoleService {

    private static RoleRepository roleRepository;

    public RoleService(RoleRepository roleRepository) {
        this.roleRepository = roleRepository;
    }

    public Set<Role> findRoleByName(String[] name) {
        Set<Role> set = new HashSet<>();
        for (String roleName : name) {
            set.add(roleRepository.getRoleByName(roleName));
        }
        return set;
    }
}
