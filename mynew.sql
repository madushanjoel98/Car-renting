Select DISTINCT vehi_type from packer where package_type='day';
Select DISTINCT package_name from packer where package_type='day' and vehi_type='Jeep(WP)';
Select pack_price from packer where package_type='day' and vehi_type='Small car' and package_name='200km 8hr';
Select hours,km from packer where  package_type='day' and vehi_type='Small car' and package_name='200km 8hr';
