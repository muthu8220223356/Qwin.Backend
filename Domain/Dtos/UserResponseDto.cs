﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class UserResponseDto: UserRequestDto
    {
        public int Id { get; set; }
    }
}
